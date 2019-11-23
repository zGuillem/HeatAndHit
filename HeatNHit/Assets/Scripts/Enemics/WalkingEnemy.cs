using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{
    // Start is called before the first frame update
    override protected void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        AttackRadius = 3f;
        Velocity = 0.2f;
        Anim = transform.GetComponent<Animator>();
        TimeBetweenAttacks = 5f;
        MaxLifePoints = 30f;
        LifePoints = MaxLifePoints;
        target = PlayerManager.instance.transform;
        AttackTimer = 0f;
    }

}
