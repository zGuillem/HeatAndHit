using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager : MonoBehaviour
{
    public float SpawnTimer;
    private float timer;
    private float angleDisplacement = 30;

    
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
            SpawnEnemy();
            timer = SpawnTimer;
            //transform.rotation = Quaternion.AngleAxis(angleDisplacement, Vector3.up);
            transform.Rotate(0.0f, angleDisplacement, 0.0f, Space.World);
        }
    }

    protected void UpdateTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            timer = 0;
        }
    }
    public void SpawnEnemy()
    {
        int SelectedSpawner = Random.Range(0, transform.childCount-1);
        Transform spawner = transform.GetChild(SelectedSpawner);
        spawner.GetComponent<SpawnEnemies>().SpawnEnemiesFunction();
    }
}
