using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloryKill : MonoBehaviour
{
    [SerializeField] private GameObject enemy;
    private Vector3 startPos;
    private Vector3 endPos;
    private float distance; //distancia entre el jugador i l'enemic
    private float lerpTime; //temps que triga en recorre la distancia
    private const float velocity = 46f; //velocitat de moviment
    private float currentLerpTime = 0f; //temps transcorregut des de l'inici de la glory kill
    private bool gloryKill = false; //variable d'activacio de la glory kill
    private const float distanciaARestar = 2.2f; //distancia que es deixa entre l'enemic i el jugador al acabar
                                                 //Huria de ser en funcio del tipus d'enemic
    private float percentatgeDistancia; //percentatge que representa 'distanciaARestar' respecte 'distance'

    // Start is called before the first frame update
    void Start() {
        //startPos = this.transform.position;
        endPos = enemy.transform.position; //Amb mes d'un enemic s'ha de triar a quin es vol fer
    }

    // Update is called once per frame
    void Update() {
        this.initGloryKill();

        this.gloryKillExecution();
    }

    void initGloryKill () {
        //Pre: --
        //Post: inicialitza les variables

        if (Input.GetButtonDown("Jump") && !gloryKill/*&& visible en la camara*/) {
            //Si hem apretat el boto i no estaba activada ja la glory kill s'inicialitza

            startPos = this.transform.position; //Si el script solo se activa en el momento de la glory kill, esto puede estar en el start
            gloryKill = true;
            distance = Mathf.Sqrt(Mathf.Pow((endPos.x - startPos.x), 2) + Mathf.Pow((endPos.z - startPos.z), 2));
            lerpTime = distance / velocity;
            //distance -= distanciaARestar;
            percentatgeDistancia = (distanciaARestar * 100 / distance) / 100;
        }
    }

    void gloryKillExecution () {
        //Pre: --
        //Post: executa la glory kill

        if (gloryKill) {
            //Si la glory kill ha estat activada

            currentLerpTime += Time.deltaTime;
            if (currentLerpTime >= lerpTime) {
                currentLerpTime = lerpTime;
            }

            float perc = currentLerpTime / lerpTime;
            if (perc >= 1 - percentatgeDistancia) {
                perc = 1 - percentatgeDistancia; //si arriba a 1 es quedara en el centre de l'enemic (dintre el model)
            }
            
            this.transform.position = Vector3.Lerp(startPos, new Vector3(endPos.x, startPos.y, endPos.z), perc);
            //activar animacio
            //Una vegada acabada la glory kill (animacio) el bool gloryKill s'ha de canviar a false
        }
    }
}
