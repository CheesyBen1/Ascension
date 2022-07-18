using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushPlayer : NetworkBehaviour
{
    public Rigidbody rb;

    [SerializeField]
    public float pushMultiplier = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("Sphere hit player!");
            pushPlayer(collision.transform.gameObject, rb.velocity * pushMultiplier);
        }
    }

    [Command(requiresAuthority = false)]
    private void pushPlayer(GameObject target, Vector3 pushAmount)
    {
        
        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();

        target.GetComponent<SyncPlayerPush>().pushed(opponentIdentity.connectionToClient, pushAmount);
    }

    //[TargetRpc]
    //public void pushed(NetworkConnection target, Vector3 pushAmount)
    //{
    //    rb.velocity += pushAmount;
    //    Debug.Log($"Pushed!");
    //}

    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        Debug.Log("Sphere hit player!");
    //        collision.transform.position += rb.velocity;
    //    }

    //}
}
