using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Enemy
{

    protected override void Constructor()
    {
        AttackRadius = 3f;
        Velocity = 0.2f;
        TimeBetweenAttacks = 5f;
        MaxLifePoints = 10f;
        AttackTimer = 0f;
        base.Constructor();
    }
}
