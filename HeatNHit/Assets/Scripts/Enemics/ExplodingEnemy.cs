using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    public GameObject explosion;
    protected override void Constructor()
    {
        AttackRadius = 3f;
        Velocity = 0.2f;
        TimeBetweenAttacks = 5f;
        MaxLifePoints = 10f;
        AttackTimer = 0f;
        base.Constructor();
    }

    public override void AddKnockBack(float Power)
    {
        base.AddKnockBack(Power);
        Knockback.y = 0;
    }

    public override void OnAttackFinish()
    {

        Explode();
        
    }

    public void Explode()
    {
        Instantiate(explosion, transform.position, transform.rotation, transform.parent);
        Die();
    }
}
