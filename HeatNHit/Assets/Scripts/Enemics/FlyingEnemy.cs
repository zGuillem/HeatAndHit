﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : Enemy
{
    /*[Header("Attack Animation Controller")]
    public AnimationCurve AttackAnimationForward;
    public AnimationCurve AttackAnimationY;*/
    [SerializeField]
    private float AnimationDuration = 2;
    [SerializeField]
    private float AnimationTime = 0; //Temps que porta en execució l'animació

    private Vector3 originalPosition; //Vector que guarda la posició original del rigidbody, pels calculs de l'animació

    [SerializeField]
    private float Altura = 6;


    override protected void Constructor()
    {
        AttackRadius = 15f;
        Velocity = 0.2f;
        TimeBetweenAttacks = 5f;
        MaxLifePoints = 20f;
        AttackTimer = 0f;
        KnockbackGrowth = 0.1f;
        base.Constructor();
    }


    override protected void MoveTowardsTarget()
    {
        base.MoveTowardsTarget();
        LevelHeight();
    }

    //Es vol que l'enemic es mogui cap el jugador i que estigui sempre a la mateixa altura
    //Com és un rigidbody que no utilitza la gravetat, però sí les col·lisions necessitem recol·locar
    //L'enemic quan aquest es desplaci per l'impacte amb altres.
    override protected Vector3 GetMoveVector()
    {
        //Calculem el moviment horitzontal de l'enemic
        Vector2 moveH = new Vector2(target.position.x - transform.position.x, target.position.z - transform.position.z);

        //Creem el vector de moviment final afegint-li el knockback que pugui tenir
        Vector3 senseKnockback = new Vector3(moveH[0], 0, moveH[1]) * Velocity;
        Vector3 finalmove = (senseKnockback + Knockback) * Time.deltaTime;
        return finalmove;
    }

    override public void OnAttackFinish()
    {
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Attacking = true;
    }

    override protected void Attack()
    {
        //StartAttackAnimation();
        Attacking = true;
        AttackTimer = TimeBetweenAttacks;                                                                                              ////No m'agrada aixo                             
        gameObject.GetComponent<Rigidbody>().isKinematic = true;
        Anim.SetTrigger("InAttackRange");
    }


    void StartAttackAnimation()
    {
        originalPosition[0] = transform.position.x;
        originalPosition[1] = transform.position.y;
        originalPosition[2] = transform.position.z;
    }

    void updateAnimationTime()
    {
        if(AnimationTime < AnimationDuration)
        {
            AnimationTime += Time.deltaTime;
        }
        else
        {
            AnimationTime = 0;
            OnAttackFinish();
        }
    }

    private void LevelHeight()
    {
        transform.Translate( new Vector3(0, Altura - transform.position.y + Knockback.y, 0) * Velocity * Time.deltaTime, Space.World);
    }


    /*
    private void DoAttackAnimation()
    {
        float newPositionY = originalPosition[1] * AttackAnimationY.Evaluate(AnimationTime);
        newPositionY = newPositionY - transform.position.y;
        float valorForward = AttackAnimationForward.Evaluate(AnimationTime);
        Vector2 direccio = transform.forward * valorForward;
        transform.Translate(new Vector3(direccio[0], newPositionY, direccio[1]), Space.World);
        updateAnimationTime();
    }
    */
}
