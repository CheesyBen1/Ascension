using Mirror;
using StarterAssets;
using UnityEngine;

public class Footsteps : NetworkBehaviour
{
    CharacterController cc;
    public AudioSource audioS;

    public float baseStepSpeed = 0.5f;
    public float sprintStepMultiplilier = 0.6f;

    private float footstepTimer = 0;

    [SyncVar]
    public bool isSprinting = false;
    private float GetCurrentOffset => isSprinting ? baseStepSpeed * sprintStepMultiplilier : baseStepSpeed;

    private StarterAssetsInputs _input;

    private GameObject playerCamera;

    public AudioClip footstepSound;

    [SyncVar]
    public Vector3 ccVel;

    [SyncVar]
    public bool isGrounded;

    [SyncVar]
    public bool playStep = false;

    // Start is called before the first frame update
    void Start()
    {
        cc = GetComponent<CharacterController>();
        _input = GetComponent<StarterAssetsInputs>();
        //playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    // Update is called once per frame
    void Update()
    {
        //if (isLocalPlayer)
        //{

        //SetVal();
        //if (isLocalPlayer)
        //{
        //    ccVel = cc.velocity;
        //    isSprinting = _input.sprint;
        //    isGrounded = cc.isGrounded;
        //}

        if (isLocalPlayer)
        {
            ccVel = cc.velocity;
            isSprinting = _input.sprint;
            isGrounded = cc.isGrounded;
            SetVal(cc.velocity, _input.sprint, cc.isGrounded);
        }
        


        HandleFootsteps();

        //}



        //if (cc.isGrounded == true && cc.velocity.magnitude > 2f && audioS.isPlaying == false)
        //{
        //   // do {
        //        audioS.volume = Random.Range(0.8f, 1);
        //        audioS.pitch = Random.Range(0.8f, 1.1f);
        //        audioS.Play();
        //   // } while (isSpr);
        //}


        //if (playStep)
        //{
        //    PlayFootsteps();
        //}
    }

    public void HandleFootsteps()
    {
        //if (!cc.isGrounded)  return; 
        //if (cc.velocity.magnitude == 0) return; 

        if (!isGrounded || ccVel.magnitude == 0)
        {

            return;
        }



        footstepTimer -= Time.deltaTime;



        if (footstepTimer <= 0)
        {

            PlayFootsteps();



            footstepTimer = GetCurrentOffset;
        }





    }

    public void PlayFootsteps()
    {
        audioS.pitch = Random.Range(0.8f, 1.1f);
        audioS.PlayOneShot(footstepSound, audioS.volume * Random.Range(0.8f, 1));
    }

    [Command(requiresAuthority = false)]
    public void SetVal(Vector3 vel, bool isSprint, bool isGround)
    {
        ccVel = vel;
        isSprinting = isSprint;
        isGrounded = isGround;
    }

    [Command(requiresAuthority = false)]
    public void SetStepBool(bool set)
    {
        playStep = set;
    }
}
