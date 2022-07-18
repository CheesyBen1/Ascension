using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using StarterAssets;
using UnityEngine.SceneManagement;

public class PlayerAnimation : NetworkBehaviour
{
    private StarterAssetsInputs _input;

    public NetworkAnimator animator;
    public Animator localAnimator;

    private PlayerMove move;

    private Sword sword;

    private bool hasSword;

    [SyncVar]
    public float x = 0;

    [SyncVar]
    public float y = 0;

    [SyncVar]
    public float jump = 0;

    private void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        move = GetComponent<PlayerMove>();
        sword = GetComponent<Sword>();
        //hasSword = sword.hasSword;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            updateAnimation();
        }
        UpdateSound();
    }

    public AudioSource audioS;
    public AudioClip swingSound;
    public AudioClip jumpSound;

    public void UpdateSound()
    {
        if(localAnimator.GetBool("sword") && localAnimator.GetBool("swordattack")){
            audioS.PlayOneShot(swingSound);
        }

        if (localAnimator.GetFloat("jump") > 0 && localAnimator.GetFloat("jump") <= 0.1f)
        {
            audioS.PlayOneShot(jumpSound);
        }
    }
    
    public void updateAnimation()
    {

        if (SceneManager.GetActiveScene().name == "3Sword Fight")
        {
            hasSword = sword.hasSword;
        }
        if (hasSword)
        {
            animator.SetTrigger("sword");
        }
        else
        {
            animator.ResetTrigger("sword");
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!hasSword)
            {
                animator.SetTrigger("push");
            }
            else
            {
                animator.SetTrigger("swordattack");
            }
            
            
        }
        else
        {
            animator.ResetTrigger("push");
            animator.ResetTrigger("swordattack");
        }

        if (!move.Grounded)
        {
            jump += 0.1f;
        }
        else if (move.Grounded)
        {
            jump -= 0.1f;

        }

        jump = Mathf.Clamp(jump, 0, 1);

        if (_input.sprint)
        {

            x = Mathf.Clamp(x, -1, 1);
            y = Mathf.Clamp(y, -1, 1);
        }
        else
        {
            x = Mathf.Clamp(x, -0.5f, 0.5f);
            y = Mathf.Clamp(y, -0.5f, 0.5f);
        }

        if (Input.GetAxis("Horizontal") != 0)
        {            
                x += Mathf.Sign(Input.GetAxis("Horizontal")) * 0.1f;                       
        }
        else
        {            
                x = 0f;            
        }

        
        if (Input.GetAxis("Vertical") != 0)
        {            
                y += Mathf.Sign(Input.GetAxis("Vertical")) * 0.1f;
        }
        else
        {            
                y = 0f;            
        }

        



        //if(Input.GetAxis("Horizontal") < 0)
        //{
        //    animator.ResetTrigger("right");
        //    animator.SetTrigger("left");

        //}
        //else if (Input.GetAxis("Horizontal") > 0)
        //{
        //    animator.ResetTrigger("left");
        //    animator.SetTrigger("right");

        //}        
        //else
        //{
        //    animator.ResetTrigger("left");
        //    animator.ResetTrigger("right");
        //}

        //if (Input.GetAxis("Vertical") > 0)
        //{
        //    animator.ResetTrigger("backwards");
        //    animator.SetTrigger("forwards");

        //}
        //else if(Input.GetAxis("Vertical") < 0)
        //{
        //    animator.ResetTrigger("forwards");
        //    animator.SetTrigger("backwards");

        //}        
        //else
        //{
        //    animator.ResetTrigger("forwards");
        //    animator.ResetTrigger("backwards");
        //}

        if (_input.sprint)
        {
            //animator.SetTrigger("running");
            x *= 2;
            y *= 2;
        }
        else
        {
            //animator.ResetTrigger("running");
            x *= 1;
            y *= 1;
        }

        if (isLocalPlayer)
        {
            localAnimator.SetFloat("walk x", x);
            localAnimator.SetFloat("walk y", y);
            localAnimator.SetFloat("jump", jump);
        }


    }

    public void resetTriggers()
    {
        animator.ResetTrigger("forwards");
        animator.ResetTrigger("backwards");
        animator.ResetTrigger("left");
        animator.ResetTrigger("right");
    }
}
