using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    [Header("Spawn Controller")]
    public AnimationCurve SpawnCurve;
    public float spawnTime;
    public float spawnRange;

    [Header("Spawnable Enemies")]
    public Transform Enemy1;
    public Transform Enemy2;
    public Transform Enemy3;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnEnemiesFunction", spawnTime, spawnTime); //canviar aquesta funcio despres, que la invocacio sigui externa
        //SpawnEnemiesFunction(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemiesFunction(float number)
    {
        float enemySpawnNumber = SpawnCurve.Evaluate(number/10)*3;

        for(float i = 0; i <= enemySpawnNumber;  i++)
        {
            Vector3 position = new Vector3(Random.Range(transform.position.x-spawnRange, transform.position.x + spawnRange),
                                            6,
                                            Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));
            //Transform enemic = Random.Range(0, transform.childCount - 1);
            Instantiate(Enemies[1], position, transform.rotation, gameObject.transform);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange * 2, 1, spawnRange*2));
    }
}
