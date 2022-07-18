using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject lookAtThing;

    // Start is called before the first frame update
    void Start()
    {
        transform.LookAt(lookAtThing.transform);   
    }

    
}
