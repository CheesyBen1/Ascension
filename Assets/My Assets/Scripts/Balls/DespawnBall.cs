using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBall : MonoBehaviour
{
    public GameObject explodeParticle;

    private void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == "Despawn Ball")
        {
            Destroy(gameObject);
        }
    }

    public float timeout = 0;
    public float timeoutTime = 100;

    public void timeoutDespawn()
    {
        if (timeout > timeoutTime)
        Destroy(gameObject);
    }

    private void Update()
    {
        timeout += Time.deltaTime;
        timeoutDespawn();
    }

    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            Instantiate(explodeParticle, transform.position, transform.rotation);
        }
    }

}
