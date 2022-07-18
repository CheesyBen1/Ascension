using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Mirror;

public class Billboard : MonoBehaviour
{
    public Transform mainCameraTransform;
    

    private void Start()
    {
        mainCameraTransform = Camera.main.transform;
    }
        private void LateUpdate()
    {
        transform.LookAt(transform.position + mainCameraTransform.rotation*Vector3.forward, mainCameraTransform.rotation*Vector3.up);
        transform.rotation = new Quaternion(0f, transform.rotation.y, 0f, transform.rotation.w);

    }


}
