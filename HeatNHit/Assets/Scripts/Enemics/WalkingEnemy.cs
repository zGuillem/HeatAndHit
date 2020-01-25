using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{

    public List<Collider> ColliderRagdoll = new List<Collider>();
    public List<Rigidbody> RigidBodyRagdoll = new List<Rigidbody>();
    public Ragdoll ragdoll;

    protected override void Constructor()
    {
        base.Constructor();
        Anim = transform.GetChild(0).GetComponent<Animator>();
        ragdoll.gameObject.SetActive(false);
    }

    override protected void Start()
    {
        base.Start();
    }

    public override void AddKnockBack(float Power)
    {
        base.AddKnockBack(Power);
        Knockback.y = 0;
    }

    private void TurnOffRagdoll()
    {
        Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
        foreach(Collider C in colliders)
        {
            if(C.gameObject != this.gameObject)
            {
                C.enabled = false;
                ColliderRagdoll.Add(C);
            }
        }
        Rigidbody[] rb = this.gameObject.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody C in rb)
        {
            if (C.gameObject != this.gameObject)
            {
                RigidBodyRagdoll.Add(C);
                C.detectCollisions = false;
               
            }
        }
    }

    protected override void Die()
    {
        TurnOnRagdoll();
    }

    private void TurnOnRagdoll()
    {
        /*
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<Rigidbody>().useGravity = false;
        foreach (Collider C in ColliderRagdoll)
        {
            if (C.gameObject != this.gameObject)
            {
                C.enabled = true;
            }
        }
        foreach (Rigidbody C in RigidBodyRagdoll)
        {
            if (C.gameObject != this.gameObject)
            {
                C.detectCollisions = true;
            }
        }*/

        ragdoll.gameObject.SetActive(true);
        ragdoll.TurnOnRagdoll(move);
    }

    public override void Death()
    {
        ragdoll.gameObject.SetActive(false);
        base.Death();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.transform == target && Attacking)
        {
            DamageTarget();
        }
    }

}
