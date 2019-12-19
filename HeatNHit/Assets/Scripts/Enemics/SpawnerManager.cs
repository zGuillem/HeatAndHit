using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{

    

    public float SpawnTimer;
    private float timer;
    private float counter = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        timer = SpawnTimer;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
        if (timer == 0)
        {
            SpawnEnemy(counter);
            timer = SpawnTimer;
        }
    }

    protected void UpdateTimer()
    {
        if (timer > 0)
        {
            timer = timer - Time.deltaTime;
        }
        else
        {
            timer = 0;
            counter += 1;
        }
    }
    public void SpawnEnemy(float EnemyNumber)
    {
        int SelectedSpawner = Random.Range(0, transform.childCount-1);
        print("SelectedSpawner: "+ SelectedSpawner);
        Transform spawner = transform.GetChild(SelectedSpawner);
        spawner.GetComponent<SpawnEnemies>().SpawnEnemiesFunction(EnemyNumber);
    }
}
