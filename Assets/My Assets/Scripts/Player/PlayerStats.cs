using Cinemachine;
using Mirror;
using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    [SyncVar]
    public float health = 100;

    public TextMeshProUGUI hpText;

    [SyncVar]
    public bool isDead = false;

    public GameObject deathCamera;

    public GameObject playerModel;

    public ParticleSystem shockwave;
    public ParticleSystem fireball;
    public ParticleSystem smoke;

    [SyncVar]
    public bool deadCamera = false;

    [SyncVar]
    public bool deadCheck = false;

    private void Start()
    {
        hpText = GameObject.FindGameObjectWithTag("HealthText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        //health -= Time.deltaTime * 10;
        if (isLocalPlayer)
        {
            checkHp();
            updateHpText();
            checkDead();
        }



    }


    private void checkHp()
    {
        if (health <= 40)
        {
            hpText.color = Color.red;
        }
        else
        {
            hpText.color = Color.green;
        }



        health = Mathf.Clamp(health, 0, 100);


    }


    private void updateHpText()
    {
        hpText.text = health.ToString("F0");
    }


    private void checkDead()
    {

        if (health <= 0)
        {
            
            
            setDead();
            //setDead();
        }

        //if (isDead == true && deadCheck == false)
        if (isDead == true)
        {
            //if (isLocalPlayer)
            //{
            //    GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = deathCamera.transform;
            //}


            //hidePlayerModel();
            //gameObject.transform.GetChild(4).gameObject.SetActive(false);

            //shockwave.Emit(1);
            //fireball.Emit(1);
            //smoke.Emit(1);
            //StartCoroutine(DeathSequence());
            //spawnExplosion();
            //hidePlayerModel();

            //GetComponent<SyncPlayerPush>().enabled = false;
            //GetComponent<Sword>().hasSword = false;

            StartCoroutine(DieAction());


        }
    }

    [Command]
    public void setDead()
    {
        isDead = true;
    }

    //[Command(requiresAuthority = false)]
    //public void setDead()
    //{
    //    isDead = true;
    //}

    public GameObject explosion;
    //IEnumerator DeathSequence()
    //{
    //    //if (isLocalPlayer)
    //    //    GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = deathCamera.transform;

    //    //NetworkClient.RegisterPrefab(explosion);
    //    //GameObject spawnBoom = Instantiate(explosion, transform.position, transform.rotation);
    //    //NetworkServer.Spawn(spawnBoom);
    //    //playerModel.SetActive(false);
    //    spawnExplosion();
    //    hidePlayerModel();



    //    yield return new WaitForSeconds(1.5f);

    //    //deadCamera = true;

    //    //disablePlayer();

    //    //GetComponent<CharacterController>().enabled = false;
    //    //GetComponent<BasicRigidBodyPush>().enabled = false;
    //    //GetComponent<PlayerInput>().enabled = false;
    //    //GetComponent<PlayerMove>().enabled = false;

    //    //for (int i = 0; i < transform.childCount; i++)
    //    //{
    //    //    transform.GetChild(i).gameObject.SetActive(false);
    //    //}

    //}

    IEnumerator DieAction()
    {
        if (deadCheck == false)
        {
            spawnExplosion();
            hidePlayerModel();

            GetComponent<SyncPlayerPush>().enabled = false;
            GetComponent<Sword>().hasSword = false;
            yield return null;

            GameObject.FindGameObjectWithTag("PlayerFollowCamera").GetComponent<CinemachineVirtualCamera>().Follow = gameObject.transform.GetChild(1);
            GetComponent<SyncPlayerPush>().enabled = false;
            GetComponent<Sword>().hasSword = false;
            deadCheck = true;
        }



    }


    [Command(requiresAuthority = false)]
    public void spawnExplosion()
    {
        NetworkClient.RegisterPrefab(explosion);
        GameObject spawnBoom = Instantiate(explosion, transform.position, transform.rotation);
        NetworkServer.Spawn(spawnBoom);
    }


    public void hidePlayerModel()
    {
        GameObject[] deathSpawns = GameObject.FindGameObjectsWithTag("deadSpawn");
        GameObject deadSpawn = deathSpawns[Random.Range(0, deathSpawns.Length - 1)];
        // gameObject.transform.position = deadSpawn.transform.position;
        //gameObject.transform.GetChild(3).gameObject.SetActive(false);
        Vector3 pos = deadSpawn.transform.position;
        Quaternion rot = deadSpawn.transform.rotation;


        //transform.position = pos;
        //transform.rotation = rot;
        //transform.position = pos;
        //transform.rotation = rot;
        //transform.position = pos;
        //transform.rotation = rot;
        //transform.position = pos;
        //transform.rotation = rot;



        for (int i = 0; i < 30; i++)
        {
            transform.position = pos;
            transform.rotation = rot;
        }

        Debug.Log("TP!");
        Debug.Log(deadSpawn.transform.position.ToString());
        //SpectatePos(gameObject.transform, deadSpawn.transform.position, deadSpawn.transform.rotation);


    }

    [Command(requiresAuthority = false)]
    public void SpectatePos(Transform target, Vector3 pos, Quaternion rot)
    {

        Transform stats = target;
        for (int i = 0; i < 10; i++)
        {
            stats.position = pos;
            stats.rotation = rot;
        }



        //RPCSpectatePos(target, pos, rot);
    }

    [ClientRpc]
    public void RPCSpectatePos(Transform target, Vector3 pos, Quaternion rot)
    {
        if (pos != null && rot != null)
        {
            Transform stats = target;
            stats.position = pos;
            stats.rotation = rot;
        }
    }

    public void takeDamage(float damage)
    {
        health -= damage;
    }



    //public void disablePlayer()
    //{
    //    GetComponent<CharacterController>().enabled = false;

    //    GetComponent<PlayerInput>().enabled = false;
    //    GetComponent<PlayerMove>().enabled = false;


    //}
}
