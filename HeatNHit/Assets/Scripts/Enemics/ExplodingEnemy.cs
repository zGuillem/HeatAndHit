using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    public Explosion explosion;

    protected override void Constructor()
    {
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
        explosion.AddDamage(damage);
        Die();
    }
}
