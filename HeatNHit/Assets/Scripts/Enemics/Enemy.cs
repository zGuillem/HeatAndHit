using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public  float AttackRadius;
    public  float Velocity;
    public  Animator Anim;
    public  float TimeBetweenAttacks;

    protected Rigidbody rb;
    protected bool attacking = false;
    public float AttackTimer;

    [SerializeField]
    protected float LifePoints;

    [SerializeField]
    private Collider ColliderEnemics;

    public static float MaxLifePoints;
    
    protected Transform target;

    [SerializeField]
    protected bool Flying = false; //Potser s'hauria de treure això en quan es fagi herenci

    virtual protected void Start()
    {
        MaxLifePoints = 0;
        target = PlayerManager.instance.transform;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //Anim.ResetTrigger("InAttackRange");
        MoveTowardsTarget();
    }

    virtual protected void FaceTarget(Vector3 destination)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
    }

    virtual protected void UpdateAttackTimer()
    {
        if (AttackTimer > 0)
        {
            AttackTimer = AttackTimer - Time.deltaTime;
        }
        else
        {
            AttackTimer = 0;
        }
    }

    virtual protected void MoveTowardsTarget()
    {

        Vector3 move = GetMoveVector();
        FaceTarget(move);
        UpdateAttackTimer();

        if (IsInAttackRange())
        {
            if (CanAttack())
            {
                Attack();
            }
        }
        else
        {
            transform.Translate(move, Space.World);
        }
    }

    virtual protected Vector3 GetMoveVector()
    {
        Vector3 move = (target.position - transform.position) * Velocity * Time.deltaTime;
        return move;
    }

    virtual protected bool IsInAttackRange()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return (distance <= AttackRadius);
    }
    virtual  protected bool CanAttack()
    { 
        return (!attacking && AttackTimer == 0);
    }

    virtual protected void Attack()
    {
        attacking = true;
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Anim.SetTrigger("InAttackRange");
    }

    virtual public void OnAttackFinish()
    {
        transform.Translate(new Vector3(20, 20, 20), Space.World);
        
    }

    virtual protected void Die()
    {
        Destroy(gameObject);
    }

    virtual public void TakeDamage(float damage)
    {
        LifePoints -= damage;
        if (LifePoints <= 0)
        {
            Die();
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        Vector3 direction;
        float distance;
        
        if (Physics.ComputePenetration(
                ColliderEnemics, transform.position, transform.rotation,
                other, other.transform.position, other.transform.rotation,
                out direction, out distance))
        {
            print(direction* distance);
            //transform.Translate(direction* distance*10);
        }
    }
    */
}
