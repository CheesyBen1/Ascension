using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnBall : NetworkBehaviour
{
    public GameObject ball;
    public float ballSpeed = 0.0f;

    public float spawnInterval = 3.0f;

    public void ClientConnect()
    {
        NetworkClient.RegisterPrefab(ball);
        
    }

    private void Start()
    {
        spawnBall();
    }

    public float timeLeft;

    private void Update()
    {
        if (timeLeft < spawnInterval)
        {
            timeLeft += Time.deltaTime;
        }
        else
        {
            //spawnBall();
            randomSpawn();
            timeLeft = 0;
            
        }

    }

    public float number = 0;

    public void randomSpawn()
    {
        number = (int)Random.Range(1, 4);

        if (number  != 1)
        {
            spawnBall();
        }
        
    }

    [Command(requiresAuthority = false)]
    public void spawnBall()
    {
        if (ball != null)
        {
            Vector3 spawnPos = transform.position + transform.forward * 2;
            Quaternion spawnRot = transform.rotation;

            
            GameObject Prefab = Instantiate(ball, spawnPos, spawnRot);
            Prefab.GetComponent<Rigidbody>().velocity += new Vector3(0, 0, ballSpeed);
            NetworkServer.Spawn(Prefab);
            
        }

    }
}
