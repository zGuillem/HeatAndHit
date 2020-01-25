using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{

    public float DuracioRagdoll;
    public float timer;
    public WalkingEnemy enemy;
    public Transform ModelRig;
    public Transform EnemyRig;

    public void TurnOnRagdoll(Vector3 move)
    {
        CopyTransformData(EnemyRig, ModelRig, move);
        timer = DuracioRagdoll;
        enemy.gameObject.SetActive(false);
    }

    private void CopyTransformData(Transform sourceTransform, Transform destinationTransform, Vector3 move)
    {
        if(sourceTransform.childCount != destinationTransform.childCount)
        {
            print("Error en el CopyTransformData");
            return;
        }
        for(int i = 0; i < sourceTransform.childCount; i++)
        {
            Transform source = sourceTransform.GetChild(i);
            Transform destination = destinationTransform.GetChild(i);
            destination.position = source.position;
            destination.rotation = source.rotation;
            Rigidbody rb = destination.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = move;
            }
            CopyTransformData(source, destination, move);
        }
    }


    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer < 0)
        {
            TurnOffRagdol();
        }
    }

    public void TurnOffRagdol()
    {
        enemy.gameObject.SetActive(true);
        enemy.Death();
        Destroy(this.gameObject);
    }
}
