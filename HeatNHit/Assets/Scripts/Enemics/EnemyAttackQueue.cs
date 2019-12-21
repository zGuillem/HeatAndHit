using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackQueue : MonoBehaviour
{
    //Declarem una Queue d'enemics
    Queue<Enemy> Queue = new Queue<Enemy>();
    public int nEnemics = 0;
    public bool CoroutineStarted = false;
    public float timer = 0;
    public float MaxTime;

    /** SINGLETON **/
    #region Singleton
    public static EnemyAttackQueue instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    /***************/

    private void Update()
    {

            UpdateTimer();
    }

    /* Afageix un enemic a la Queue*/
    public void AddEnemyToQueue(Enemy e)
    {
        if (!Queue.Contains(e))
        {
            Queue.Enqueue(e);
            nEnemics++;
        }
    }

    public void UnqueueEnemy()
    {
        print("LetEenymAttack");
        if (Queue.Peek() != null)
        {
            Queue.Peek().GrantPermisionForAttack();
        }
        Queue.Dequeue();
        nEnemics--;
    }

    public void UpdateTimer()
    {
        timer += Time.deltaTime;
        if (timer > MaxTime)
        {
            if(nEnemics > 0)
            {
                UnqueueEnemy();
            }
            timer = 0;
        }
    }
    

}
