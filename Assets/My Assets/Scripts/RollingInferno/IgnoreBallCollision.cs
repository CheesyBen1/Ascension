using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBallCollision : MonoBehaviour
{
    public GameObject prefab;

    private void Start()
    {
        Physics.IgnoreCollision(prefab.GetComponent<Collider>(), GetComponent<Collider>());
    }
}
