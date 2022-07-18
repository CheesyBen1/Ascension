using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public GameObject headFollow;

    // Update is called once per frame
    void Update()
    {
        transform.position = headFollow.transform.position;
    }
}
