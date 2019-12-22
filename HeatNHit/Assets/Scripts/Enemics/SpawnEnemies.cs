using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    [Header("Spawn Controller")]
    public AnimationCurve SpawnCurve;
    public float spawnRange;
    public int ComptadorRondes; //Contador de les vegades que s'ha superat el nombre de rondes dintre el graf
    public int xinCurve = 1; //Valor que representa la X en el graf

    [Header("Valors orientatius, canviar no afectarà en res")]
    public float MaxEnemies;    //Valor MAXIM d'enemics que spawnejen, només és orientatiu, tot es controla per la curva, canviar el valor no afectarà en res
    public float MinEnemies;    //Valor MINIM d'enemics que spawnejen, només és orientatiu, tot es controla per la curva, canviar el valor no afectarà en res
    public float MaxRondes;     //Quantitat de rondes controlades per l'spawn, només és orientatiu, tot es controla per la curva, canviar el valor no afectarà en res
    

    [Header("Spawnable Enemies")]
    public Transform Enemy1;
    public Transform Enemy2;
    public Transform Enemy3;
    public int nombreEnemics = 2;
    public Transform[] Enemics = new Transform[2];

    // Start is called before the first frame update
    void Start()
    {
        Enemics = new Transform[2];
        Enemics[0] = Enemy1;
        Enemics[1] = Enemy2;
        //Enemics[2] = Enemy3;
        //InvokeRepeating("SpawnEnemiesFunction", spawnTime, spawnTime); //canviar aquesta funcio despres, que la invocacio sigui externa
        //SpawnEnemiesFunction(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnEnemiesFunction()
    {
        xinCurve++;
        if (xinCurve > MaxRondes)
        {
            ComptadorRondes++;
            xinCurve = 1;
        }

        int enemySpawnNumber = (int)SpawnCurve.Evaluate(xinCurve) +ComptadorRondes;
        for (float i = 0; i <= enemySpawnNumber;  i++)
        {
            int enemic = Mathf.FloorToInt(Random.Range(0, 4));

            //FER QUE SURTIN MÉS TERRESTRES I MENYS VOLADORS
            enemic = Mathf.Clamp(enemic, 0, 1);


            Vector3 position = new Vector3(Random.Range(transform.position.x-spawnRange, transform.position.x + spawnRange),
                                            Enemics[enemic].position.y,
                                            Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));
            
            Instantiate(Enemics[enemic], position, transform.rotation, transform.parent.parent.parent);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange * 2, 1, spawnRange*2));
    }
}
