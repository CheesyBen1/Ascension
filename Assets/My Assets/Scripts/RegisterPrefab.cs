using UnityEngine;
using Mirror;

public class RegisterPrefab : NetworkBehaviour
{
    public GameObject ball;

    // Register prefab and connect to the server  
    public void ClientConnect()
    {
        NetworkClient.RegisterPrefab(ball);
       // NetworkClient.RegisterHandler<ConnectMessage>(OnClientConnect);
       // NetworkClient.Connect("localhost");
    }

    //void OnClientConnect(ConnectMessage msg)
    //{
    //    Debug.Log("Connected to server: " + conn);
    //}


    public void Update()
    {
        if (Input.GetKeyDown("e"))
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
            NetworkServer.Spawn(Prefab);
        }

    }

}