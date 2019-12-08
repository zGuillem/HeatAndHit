using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gunBasic : MonoBehaviour
{
    public float damageMin = 10f;
    public float damageMax = 10f;
    public float range = 100f;
    public float fireRate = 10f;
    public float impactForce = 0.01f;

    public Camera fpsCam;

    public float screenShakeForce = 0.025f;
    public float screenShakeTime = 0.1f;

    public float recoilForce = 0.5f;

    public float heatStep;
    public Material cold;
    public Material hot;
    public AnimationCurve curve;
   
    public ParticleSystem muzzleFlash;
    public GameObject impactParticle;


    private Renderer rend;
    private float heat = 0f;
    private float damage;
    private float nextTimeToFire = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material = cold;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        
        if(!Input.GetButton("Fire1"))
        {
            heat *= 0.95f;
        }

        rend.material.Lerp(cold, hot, curve.Evaluate(heat));
    }

    void Shoot()
    {
        RaycastHit hit;

        //Calcula8r damage respecte temperatura
        damage = (damageMax-damageMin) * curve.Evaluate(heat) + damageMin; //Definir segons la funcio que volguem utilitzar pel calor i com crear-la.
        heat += heatStep;

        Debug.Log(curve.Evaluate(heat));
        

        //Feedback jugador
        CameraShaker.Instance.Shake(screenShakeTime, screenShakeForce);
        muzzleFlash.Play();
        

        //Calcular fisiques del raycast
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            target _target = hit.transform.GetComponent<target>();
            if(_target != null)
            {
                _target.takeDamage(damage, impactForce, hit.point, hit.normal);
            }
        }

        //Feedback post dispar
        fpsCam.GetComponent<CamLook>().Recoil(recoilForce);
        GameObject impactGo = Instantiate(impactParticle, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGo, 1);
    }

}
