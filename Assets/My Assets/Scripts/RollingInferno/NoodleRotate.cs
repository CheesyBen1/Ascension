using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoodleRotate : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public GameObject pointObject;
    public float rotateSpeed = 10;
    public float pushForce = 5;

   

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(pointObject.transform.position, axis, rotateSpeed * Time.deltaTime);

       
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        collision.gameObject.GetComponent<Rigidbody>().velocity += new Vector3(rotateSpeed * pushForce, 0, 0);
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Player")
        {
            Vector3 forceDirection = other.gameObject.transform.position - transform.position;
            forceDirection.Normalize();

            Debug.Log("Noddle hit player");
            other.transform.GetComponent<Rigidbody>().velocity += forceDirection*pushForce*rotateSpeed;
        }
    }
}
