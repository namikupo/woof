using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Test");

        if (other.gameObject.CompareTag("trampoline"))
        {
            Rigidbody rb = GetComponentInParent<Rigidbody>();
            rb.AddForce(0f,50f,0f);
            Debug.Log(rb.gameObject.name);
        }
    }
}
