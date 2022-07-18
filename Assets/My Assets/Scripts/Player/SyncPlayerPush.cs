using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//[RequireComponent(typeof(Rigidbody))]
public class SyncPlayerPush : NetworkBehaviour
{
    [SyncVar]
    public bool isPushed = false;

   


    public GameObject playerCamera;
    public int rayCastDist = 2;

    
    public Rigidbody rb;
 

    [SyncVar]
    public Vector3 testVector;

    Vector3 currentPos;
    Vector3 targetPos;

    Vector3 ClientPushedAmount;

    public float pushForce = 0.2f;

    public AudioSource audioS;

    public AudioClip pushSound;

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        if(gameObject.GetComponent<Rigidbody>() != null)
             rb = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //if (isPushed)
        //{
        //    pushPlayer(transform.forward * 2);

        //}

        RaycastHit hit;
        
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, rayCastDist))
        {
            if (hit.collider.tag == "Player")
            {
                Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log("Did Hit player");
                if (Input.GetButtonDown("Fire1"))
                {
                    pushPlayer(hit.transform.gameObject, playerCamera.transform.forward * pushForce);
                    PushSound();
                }
            }
        }


        if(SceneManager.GetActiveScene().name == "Rolling Inferno")
        {
            pushForce = 0.2f;
        }
        //transform.position += rb.velocity;

        //transform.position += ClientPushedAmount * Time.deltaTime;
        //transform.position = Vector3.Lerp(currentPos, targetPos, 1);

        //isPushed = false;
    }

    [Command (requiresAuthority = false)]
    private void pushPlayer(GameObject target, Vector3 pushAmount)
    {
        //target.transform.position += pushAmount;
        //target.GetComponent<Rigidbody>().velocity = pushAmount;
        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();

        pushed(opponentIdentity.connectionToClient, pushAmount);
    }

    [TargetRpc]
    public void pushed(NetworkConnection target, Vector3 pushAmount)
    {
        //testVector = pushAmount;
        //ClientPushedAmount = pushAmount;
        //currentPos = transform.position;
        //targetPos = currentPos + pushAmount;
        //transform.position += Vector3.Lerp(currentPos, targetPos, 1);


        target.identity.GetComponent<Rigidbody>().velocity += pushAmount;
        PushSound();
        Debug.Log($"Pushed!");
    }

    public void PushSound()
    {
        audioS.pitch = 0.3f;
        audioS.PlayOneShot(pushSound);
        audioS.pitch = 1.0f;
    }



    //public void getPushed(Vector3 pushAmount)
    //{
    //    Vector3 currentPos = transform.position;
    //    Vector3 targetPos = currentPos + pushAmount;
    //    transform.position += Vector3.Lerp(currentPos, targetPos, 1);
    //}
}
