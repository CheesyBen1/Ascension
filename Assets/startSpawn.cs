using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startSpawn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(despawn());
    }

    IEnumerator despawn()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
