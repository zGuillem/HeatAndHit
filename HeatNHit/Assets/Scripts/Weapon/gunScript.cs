using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode] //See changes on editor
public class gunScript : MonoBehaviour
{
    //Variables publiques ------------------------------------------------
    public enum TypeOfWeapon
    {
        Gun,
        Melee
    }

    public TypeOfWeapon typeOfWeapon = TypeOfWeapon.Gun;

    //State controller
    public enum InitialState
    {
        Functional,
        Broken
    }

    [Header("State controller")]

    public InitialState iState = InitialState.Functional;
    public bool activada = true;

    //Weapon characteristics  
    [Header("Weapon Characteristics")]
    public float damageMin;                                                 //Minimum damage, when the bullet is cold
    public float damageMax;                                                 //Maximum damage, when the bullet is hot
    public float range;                                                     //Maximum distance that the gun can reach
    public float fireRate;                                                  //Controls the fire rate of the gun, shoots per second
    public float impactForce;                                               //Force of impact per bullet
    public int numberOfBullets;                                             //Number of bullets per shot
    public float aimOffset;                                                 //Difference in direction of the bullets to prevent them from going straight

    //Shooting feedback
    [Header("Feedback")]
    public Camera fpsCam;                                                   //Acces to the player camera
    public ParticleSystem muzzleFlash;                                      //Particles tip of the gun
    public GameObject impactParticle;                                       //Particles from impacts
    public float recoil;                                                    //Recoil of the gun after shooting
    public float screenShakeForce;                                          //Screen shake force after shooting
    public float screenShakeTime;                                           //Screen shake time after shooting
    public weaponAudio audioPlayer;
    public Animator animationPlayer;


    //Heat treatment
    [Header("Heat Treatment")]
    public float heatStep;                                                  //Heat per shoot
    public float heatDecreaseStep;                                          //Heat decreased by second if not shooting
    public float brokenHeatDecreasedStep;                                   //Heat decreased by second when broken.
    public AnimationCurve heatCurve;                                        //Curve of heat. Instead of using a linear function, we use a curve.
    public Material cold;                                                   //Material of the model when cold
    public Material hot;                                                    //Material of the model when hot
    public Light heatLight;

    //Variables Privades -------------------------------------------------
    //State controller
    private delegate void StatePlayer();                                    //Variable que guardara quin estat de l'arma executar.
    StatePlayer stateUpdate;

    private delegate void ShootingMethod(float damage);  //Variable que guardara quina manera d'atacar té l'arma.
    ShootingMethod shootingMethod;

    //Weapon characteristics
    private float nextTimeToFire = 0f;                                      //Next time that the weapon can fire, in seconds

    //Heat treatment
    private float heat = 0f;                                                //Saves the heat of the gun
    private Renderer rend;                                                  //Acces to the render


    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = cold;

        switch (iState)
        {
            case InitialState.Functional:
                stateUpdate = FunctionalStateUpdate;
                break;
            case InitialState.Broken:
                stateUpdate = BrokenStateUpdate;
                break;
        }

        switch(typeOfWeapon)
        {
            case TypeOfWeapon.Gun:
                shootingMethod = ShootingMethodGun;
                break;
            case TypeOfWeapon.Melee:
                shootingMethod = ShootingMethodMelee;
                break;
        }

        setArmaMesh(activada); 
    }

    // Update is called once per frame
    void Update()
    {
        stateUpdate();
    }

    //State machine

    void FunctionalStateUpdate()
    {
        if (activada)
        {
            if (!Input.GetButton("Fire1"))
                HeatManagement(-heatDecreaseStep * Time.deltaTime);
            else if (Time.time >= nextTimeToFire) // && Input.GetButton("Fire1")
            {
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
            }

            UpdateMaterial();
        }
        else
            HeatManagement(-heatDecreaseStep * Time.deltaTime);
    }

    void BrokenStateUpdate()
    {
        HeatManagement(-brokenHeatDecreasedStep * Time.deltaTime);
        if (activada)
            UpdateMaterial();
    }

    //Attacking
    void Shoot()
    {
        HeatPreShooting();
        FeedbackPreShooting();
        shootingMethod(DmgPerActualHeat());
        FeedbackPostShooting();
        HeatPostShoting();
    }

    void ShootingMethodGun(float damage)
    {
        for(int i = 0; i < numberOfBullets; i++)
        {
            ShootABullet(fpsCam.transform.position, fpsCam.transform.forward + generateRandomAimOffset(), damage);
        }
    }

    void ShootingMethodMelee(float damage)
    {
        StartCoroutine(MeleeAttack(damage));
    }

    private IEnumerator MeleeAttack(float damage)
    {
        float waitTime = Time.time + 1f;           //Duracio de l'atac de l'espasa

        while(Time.time < waitTime)
        {
            ShootABullet(transform.position, transform.forward, damage);
            yield return null;
        }

        
    }

    void ShootABullet(Vector3 fromPoint, Vector3 direction, float damage)
    {
        RaycastHit hit;

        if (Physics.Raycast(fromPoint, direction, out hit, range))
        {
            //Debug.DrawLine(fromPoint, hit.point, Color.black);
            target _target = hit.transform.GetComponent<target>();
            if (_target != null)
                _target.takeDamage(damage, impactForce, hit.point, hit.normal);
        }

        if (hit.normal != Vector3.zero)
        {
            GameObject impactGo = Instantiate(impactParticle, hit.point + hit.normal * 0.1f, Quaternion.LookRotation(hit.normal));
            Destroy(impactGo, 1);
        }
    }

    //Feedback
    void FeedbackPreShooting()
    {
        muzzleFlash.Play();
    }

    void FeedbackPostShooting()
    {
        if (animationPlayer != null)
            animationPlayer.SetTrigger("StartAttack");


        CameraShaker.Instance.Shake(screenShakeTime, screenShakeForce);
        fpsCam.GetComponent<CamLook>().Recoil(recoil);

        audioPlayer.playShoot();
    }

    //Heat treatment
    void HeatPreShooting()
    {

    }

    float DmgPerActualHeat()
    {
        return (damageMax - damageMin) * heatCurve.Evaluate(heat) + damageMin;
    }

    void HeatPostShoting()
    {
        HeatManagement(heatStep);
    }

    void UpdateMaterial()
    {

        rend.material.Lerp(cold, hot, heatCurve.Evaluate(heat));
        //heatLight.intensity = heatCurve.Evaluate(heat);
    }

    void HeatManagement(float variacio)
    {
        heat += variacio;
        heat = Mathf.Clamp(heat, 0, 1);

        if (heat > 0 && heat < 1)
            return;

        if (heat == 1 && stateUpdate != BrokenStateUpdate)
        {
            stateUpdate = BrokenStateUpdate;
            audioPlayer.playBurnOut();
        }
        else if (heat == 0 && stateUpdate != FunctionalStateUpdate)
        {
            stateUpdate = FunctionalStateUpdate;
            audioPlayer.plaplayColdOut();
        }
            
    }

    //Activate or desactivate weapon

    public void setWeaponState(bool activate)
    {
        setArmaMesh(activate);
        activada = activate;
    }

    public void setArmaMesh(bool activada)
    {
        GetComponent<MeshRenderer>().enabled = activada;
    }

    //Extra
    Vector3 generateRandomAimOffset()
    {
        return new Vector3(Random.Range(-aimOffset, aimOffset), Random.Range(-aimOffset, aimOffset), Random.Range(-aimOffset, aimOffset));
    }
}
