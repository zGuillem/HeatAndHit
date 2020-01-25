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

    [Header("Control de rondes")]
    public float timerIniciRonda;
    public float timerRonda;
    public float duracioIniciRonda;
    public float DuracioTimerRonda;
    public float enemicsVius = 0;

    [Header("Valors orientatius, canviar no afectarà en res")]
    public float MaxEnemies;    //Valor MAXIM d'enemics que spawnejen, només és orientatiu, tot es controla per la curva, canviar el valor no afectarà en res
    public float MinEnemies;    //Valor MINIM d'enemics que spawnejen, només és orientatiu, tot es controla per la curva, canviar el valor no afectarà en res
    public float MaxRondes;     //Quantitat de rondes controlades per l'spawn, només és orientatiu, tot es controla per la curva, canviar el valor no afectarà en res
    

    [Header("Spawnable Enemies")]
    public Transform Enemy1;
    public Transform Enemy2;
    public Transform Enemy3;
    public Transform[] Enemics;
    [Header("Spawnable Enemies Ratio")]
    public int Enemy1Ratio;
    public int Enemy2Ratio;
    public int Enemy3Ratio;




    public static SpawnEnemies instance;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        Enemics = new Transform[3];
        Enemics[0] = Enemy1;
        Enemics[1] = Enemy2;
        Enemics[2] = Enemy3;
        timerIniciRonda = duracioIniciRonda;
        timerRonda = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIniciRonda != 0) //Si la ronda ha començat
        {
            timerIniciRonda -= Time.deltaTime;
            if (timerIniciRonda < 0)
            {
                StartRound();
            }
        }
        else if(timerRonda != 0)
        {
            timerRonda -= Time.deltaTime;
            if(timerRonda < 0)
            {
                FinishRound();
            }
        }
        
    }

    public void SpawnEnemiesFunction()
    {
        xinCurve++;
        if (xinCurve > MaxRondes)
        {
            ComptadorRondes++;
            xinCurve = 1;
        }

        enemicsVius += (int)SpawnCurve.Evaluate(xinCurve) + ComptadorRondes;
        for (float i = 0; i < enemicsVius;  i++)
        {
            int enemic = Mathf.FloorToInt(Random.Range(0, 100));
            int valorPercentual = 0;
            if (enemic >= valorPercentual && enemic <= valorPercentual+Enemy1Ratio)
            {
                Vector3 position = new Vector3(Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange),
                                            Enemics[0].position.y,
                                            Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));

                Instantiate(Enemics[0], position, transform.rotation, transform.parent.parent.parent);
            }
            else
            {
                valorPercentual += Enemy1Ratio;
                if (enemic >= valorPercentual  && enemic <= valorPercentual + Enemy2Ratio)
                {
                    Vector3 position = new Vector3(Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange),
                                            Enemics[1].position.y,
                                            Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));

                    Instantiate(Enemics[1], position, transform.rotation, transform.parent.parent.parent);
                }
                else
                {
                    valorPercentual += Enemy2Ratio;
                    if (enemic >= valorPercentual && enemic <= valorPercentual + Enemy3Ratio)
                    {
                        Vector3 position = new Vector3(Random.Range(transform.position.x - spawnRange, transform.position.x + spawnRange),
                                            Enemics[2].position.y,
                                            Random.Range(transform.position.z - spawnRange, transform.position.z + spawnRange));

                        Instantiate(Enemics[2], position, transform.rotation, transform.parent.parent.parent);
                    }
                }
            }

            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange * 2, 1, spawnRange*2));
    }

    public void removeEnemy()
    {
        enemicsVius--;
        if (enemicsVius == 0)
        {
            FinishRound();
        }
    }

    public void StartRound() //Conta abans de fer spawn d'enemics
    {
        timerIniciRonda = 0;
        timerRonda = DuracioTimerRonda;
        SpawnEnemiesFunction();
    }

    public void FinishRound()
    {
        timerIniciRonda = duracioIniciRonda;
        timerRonda = 0;
    }
}
