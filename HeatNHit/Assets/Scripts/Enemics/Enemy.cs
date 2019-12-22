using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* Super classe controladora dels Enemics, 
 * ExplodingEnemy, FlyingEnemy i WalkingEnemy hereten d'aquesta
 */
public class Enemy : MonoBehaviour
{

    [Header("Enemy properties")]
    public float Velocity;
    public Animator Anim;                                                                        //Controlador de les animacions
    [SerializeField]
    protected float LifePoints;                                                                 //Punts de vida actuals de l'enemic
    [SerializeField]
    protected EnemyAttackQueue EAQ;                                                             //Sistema de control de cua
    public float MaxLifePoints;                                                                 //Punts de vida màxims de l'enemic
    public int score;
    public bool dead;

    [Header("Attack properties")]
    public float damage;
    public  float AttackRadius;                                                                 //AttackRange                           
    public  float TimeBetweenAttacks;
    [SerializeField]
    protected bool Attacking = false;                                                            //Indica si el personatge ha atacat fa poc i 
                                                                                                //s'ha d'esperar per poder tornar a atacar
    public float AttackTimer;                                                                   //Temps entre atacs
    protected Transform target;                                                                 //Objectiu

    [Header("Particles")]
    public ParticleSystem deathParticles;

    [Header("Knockback properties")]
    [SerializeField]
    protected Vector3 Knockback;                                                                //Retrocés que té un enemic després de ser disparat
    [SerializeField]
    protected float KnockbackGrowth;                                                            //Rati amb el que disminueix el Knockback
    [SerializeField]
    protected float KnockbackSensitivity;                                                       //Multiplicador, indica la sensibilitat de l'enemic al knockback
    virtual protected void Start()
    {
        Constructor();
    }

    virtual protected void Constructor()
    {
        target = PlayerManager.instance.transform;                                              //definim l'objectiu
        EAQ = EnemyAttackQueue.instance;
        Anim = transform.GetComponent<Animator>();
        LifePoints = MaxLifePoints;
        target = PlayerManager.instance.transform;
        AttackTimer = -1;
    }

    // Update is called once per frame
    virtual protected void Update()
    {
        //Anim.ResetTrigger("InAttackRange");
        if (!dead)
        {
            MoveTowardsTarget();
            UpdateAttackTimer();
            UpdateKnockback();
        }

    }

    //Girem l'enemic en la direcció del jugador
    virtual protected void FaceTarget(Vector3 destination)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    //Dibuixem informació de debug
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);
        Gizmos.DrawLine(transform.position, transform.forward);
    }

    //Actualitza el AttackTimer per controlar el temps entre atacs
    virtual protected void UpdateAttackTimer()
    {
        if (AttackTimer > 0)
        {
            AttackTimer -= Time.deltaTime;
        }
        else
        {
            AttackTimer = -1;
        }
    }

    //Desplaça l'enemic en la direcció del seu objectiu
    virtual protected void MoveTowardsTarget()
    {
        Vector3 move = GetMoveVector();

        /*
        if (IsInAttackRange())
        {
            if (CanAttack())
            {
                AskPermisionForAttack();
                //Attack();
            }
        }
        else
        {
            FaceTarget(move);
            transform.Translate(move, Space.World);
        }
        */
        if (!Attacking)
        {
            FaceTarget(move);
            if (IsInAttackRange())
            {
                if (CanAttack())
                {
                    AskPermisionForAttack();
                    //Attack();
                }
            }
            else
            {
                transform.Translate(move, Space.World);
            }
        }
        

    }

    //Aconsegueix el vector direcció cap a l'objectiu
    virtual protected Vector3 GetMoveVector()
    {
        Vector3 move = (target.position - transform.position) * Velocity * Time.deltaTime;
        move += Knockback;
        return move;
    }

    //Comprova si target està dins el rang d'atac
    virtual protected bool IsInAttackRange()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        return (distance <= AttackRadius);
    }

    //Comprova si pot atacar
    virtual  protected bool CanAttack()
    {
        return (AttackTimer == -1);
    }

    //Fa l'atac
    virtual protected void Attack()
    {
        Attacking = true;
        /*gameObject.GetComponent<Rigidbody>().isKinematic = true;*/
        Anim.SetTrigger("InAttackRange");
    }

    //Actaulitza l'enemic quan s'acaba l'animació d'atac
    //Si es treu l'animació s'ha de treure aquesta funció
    virtual public void OnAttackFinish()
    {
        Attacking = false;
        //transform.Translate(new Vector3(20, 20, 20), Space.World); 
    }

    //Controla la mort de l'enemic
    virtual protected void Die()
    {
        Rigidbody rb = transform.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        dead = true;
        Anim.SetTrigger("Death");
    }

    virtual protected void Death()
    {
        Destroy(gameObject);
        target.GetComponent<PlayerScore>().ScorePlus(score);
    }

    //Controla el rebre mal de l'enemic
    virtual public void TakeDamage(float damage)
    {
        if (LifePoints > 0)
        {
            LifePoints -= damage;
            if (LifePoints <= 0)
            {
                Die();
            }
        }
    }

    //Permet al 
    virtual public void GrantPermisionForAttack()
    {
        Attacking = true;
        Attack();
    }

    virtual public void AskPermisionForAttack()
    {
        EAQ.AddEnemyToQueue(this);
    }

    virtual public void AddKnockBack(float Power)
    {
        print("AddKnockBack");
        Knockback = (transform.position - target.position).normalized * Power * KnockbackSensitivity;
    }

    virtual protected void UpdateKnockback()
    {
        Knockback *= KnockbackGrowth;
    }

    virtual protected void DamageTarget()
    {
        target.GetComponent<PlayerHealth>().takeDamage(damage);

    }
}
