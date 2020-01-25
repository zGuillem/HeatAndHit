using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodingEnemy : Enemy
{
    public Explosion explosion;
    [Header("Control d'explosió")]
    public float ForcaExplosio;

    protected override void Constructor()
    {
        base.Constructor();
        Anim = transform.GetComponent<Animator>();
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

    protected override void AttackMode()
    {
        Attack();
    }
    public void Explode()
    {
        Vector3 posicioExplosio = new Vector3(transform.position.x, 0, transform.position.z);
        Instantiate(explosion, posicioExplosio, transform.rotation, transform.parent);
        //explosion.giveForce(ForcaExplosio);
        explosion.AddDamage(damage);
        Destroy(gameObject);
    }
}
