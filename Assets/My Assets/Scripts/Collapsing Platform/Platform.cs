

using Mirror;
using System.Collections;
using UnityEngine;

public class Platform : NetworkBehaviour
{
    [SyncVar]
    public bool isCollapse = false;

    public bool startCrumble = false;

    public bool crumble = false;
    public bool collapsing = false;

    public float speed = 0.001f;
    public float amount = 0.001f;

    public float vibrateAmount = 0.3f;

    public float collapseSpeed = 0.01f;

    public float maxCollapse;



    public Vector3 initialPos;

    public AudioSource sfx;
    public AudioClip crumbleSfx;

    private void Start()
    {
        maxCollapse = transform.position.y - 2f;
        initialPos = transform.position;

        AudioSource aS = gameObject.AddComponent<AudioSource>() as AudioSource;

        sfx = aS;

        sfx.clip = crumbleSfx;

        sfx.spatialBlend = 1;
        sfx.volume = 0.7f;
        

    }

    void Update()
    {
        if (isCollapse)
        {
            if (startCrumble == false)
            {
                StartCoroutine(Crumble());
                startCrumble = true;
            }
            if (crumble)
            {
                transform.position = initialPos;
                transform.position = initialPos + new Vector3(Random.Range(-vibrateAmount, vibrateAmount), 0, Random.Range(-vibrateAmount, vibrateAmount));
            }

            if (collapsing)
            {

                if (transform.position.y >= maxCollapse)
                {
                    transform.position += new Vector3(0, -collapseSpeed, 0);

                }
                else
                {
                    sfx.Stop();
                }
            }
        }

    }

    IEnumerator Crumble()
    {
        sfx.Play();  
        crumble = true;
        yield return new WaitForSeconds(1);
        crumble = false;
        transform.position = initialPos;
        collapsing = true;

    }
}
