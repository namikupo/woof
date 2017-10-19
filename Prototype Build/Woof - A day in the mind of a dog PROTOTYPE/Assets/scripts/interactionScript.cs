using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == ("trampoline"))
        {
            Rigidbody rb = GetComponentInParent<Rigidbody>();
            rb.isKinematic = false;
            rb.AddForce(new Vector3(0f, 500f, 0f),ForceMode.VelocityChange);
            Debug.Log(rb.gameObject.name);
            StartCoroutine(bounce());
        }
    }

    private IEnumerator bounce()
    {
        yield return new WaitForSecondsRealtime(1f);
        Rigidbody rb1 = GetComponentInParent<Rigidbody>();
        rb1.isKinematic = true;
    }
}
