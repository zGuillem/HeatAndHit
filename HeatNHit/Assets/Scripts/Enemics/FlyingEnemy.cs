using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    public AnimationCurve xx;

    [SerializeField]
    private float Altura = 6;

    // Start is called before the first frame update
    override protected void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        AttackRadius = 15f;
        Velocity = 0.2f;
        Anim = transform.GetComponent<Animator>();
        TimeBetweenAttacks = 5f;
        MaxLifePoints = 20f;
        LifePoints = MaxLifePoints;
        target = PlayerManager.instance.transform;
        AttackTimer = 0f;
    }

    override protected Vector3 GetMoveVector()
    {
        Vector3 move = (target.position - transform.position);
        Vector3 finalmove = new Vector3(move[0], Altura - transform.position.y, move[2]) * Velocity * Time.deltaTime;
        return finalmove;
    }

    override public void OnAttackFinish()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        attacking = false;
        AttackTimer = TimeBetweenAttacks;
    }
}
