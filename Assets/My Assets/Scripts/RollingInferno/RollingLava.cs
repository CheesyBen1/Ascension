using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RollingLava : NetworkBehaviour
{

    public float lavaDamage = 20;

    public PlayerStats stats;

    public AudioSource sfx;
    public AudioClip burningSfx;

    private void Start()
    {
        stats = GetComponent<PlayerStats>();

        AudioSource aS = gameObject.AddComponent<AudioSource>() as AudioSource;

        sfx = aS;

        sfx.clip = burningSfx;

        sfx.spatialBlend = 1;
        sfx.volume = 0.7f;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            if (stats.health >= 0)
            {
                //stats.health -= lavaDamage * Time.deltaTime;
                //takeLavaDamage(lavaDamage * Time.deltaTime);
                stats.takeDamage(lavaDamage * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Lava")
        {
            sfx.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Lava")
        {
            sfx.Stop();
        }
    }
    //private void OnCollisionStay(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Lava")
    //    {
    //        takeLavaDamage(lavaDamage * Time.deltaTime);
    //    }
    //}



    //[Command (requiresAuthority = false)]
    //private void takeLavaDamage(float damage)
    //{
    //    stats.health -= damage;

    //    RPCtakeDamageLava(damage);
    //}

    //[ClientRpc]
    //private void RPCtakeDamageLava(float damage)
    //{
    //    stats.health -= damage;
    //}
}
