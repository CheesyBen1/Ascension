using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignorePlayerCollision : MonoBehaviour
{
    public CharacterController characterController;

    // Update is called once per frame
    void Update()
    {
        Physics.IgnoreCollision(characterController, GetComponent<BoxCollider>()); 
    }
}
