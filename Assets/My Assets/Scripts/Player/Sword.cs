using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class Sword : NetworkBehaviour
{
    [SyncVar]
    public bool hasSword = false;

    [SyncVar]
    public bool hasSwordGold = false;

    public GameObject swordObject;
    public GameObject swordGoldObject;

    
    public GameObject playerCamera;
    public int rayCastDist = 2;

    public float damage = 10;
    public float damageGold = 20;

    public float push = 0.2f;
    public float pushGold = 0.8f;

    

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        swordObject.SetActive(false);
        swordGoldObject.SetActive(false);

        if (SceneManager.GetActiveScene().name == "3Sword Fight")
        {
            hasSword = true;
            swordObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (SceneManager.GetActiveScene().name == "3Sword Fight")
        {
            if (hasSword)
            {
                
                if (hasSwordGold)
                {
                    swordGoldObject.SetActive(true);
                    swordObject.SetActive(false);
                    damage = damageGold;
                    GetComponent<SyncPlayerPush>().pushForce = pushGold;
                }
                else
                {
                    swordObject.SetActive(true);
                    swordGoldObject.SetActive(false);
                }

                RaycastHit hit;

                if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.TransformDirection(Vector3.forward), out hit, rayCastDist))
                {
                    if (hit.collider.tag == "Player")
                    {
                        if (Input.GetButtonDown("Fire1"))
                        {
                            if (hit.transform.GetComponent<PlayerStats>().health >= 0)
                            {
                                hit.transform.GetComponent<PlayerStats>().health -= damage;
                                giveDamage(hit.transform.gameObject, damage);
                            }
                        }
                    }
                }
            }
            else
            {
                swordObject.SetActive(false);
                swordGoldObject.SetActive(false);
            }
        }
        
    }

    public AudioSource sfx;
    public AudioClip powerUp;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "GoldSword")
        {
            if (isLocalPlayer)
            {
                setGold();
            }
            Destroy(other.transform.gameObject);
            sfx.PlayOneShot(powerUp);
        }   
    }

    [Command]
    public void setGold()
    {
        hasSwordGold = true;
    }


    [Command(requiresAuthority = false)]
    public void giveDamage(GameObject target, float damage)
    {
        NetworkIdentity opponentIdentity = target.GetComponent<NetworkIdentity>();
        takeDamage(opponentIdentity.connectionToClient, damage);
    }

    [TargetRpc]
    public void takeDamage(NetworkConnection target, float damage)
    {
        PlayerStats stats = target.identity.GetComponent<PlayerStats>();


        //stats.health -= damage;

        stats.takeDamage(damage);
    }
}
