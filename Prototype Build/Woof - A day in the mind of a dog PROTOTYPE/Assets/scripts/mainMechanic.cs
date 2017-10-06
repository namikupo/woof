using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMechanic : MonoBehaviour
{

    private Transform cylinderTransform;

    [SerializeField]
    private GameObject sphere;

	void Start ()
    {
        
	}
	
	void Update ()
    {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && other.tag == "interactableObject")
        {
            Destroy(other.gameObject);
            Instantiate(sphere, other.transform.position, other.transform.rotation);
        }
        else if (Input.GetKey(KeyCode.Mouse1) && other.tag == "draggable")
        {
            Collider collider = other.gameObject.GetComponent<Collider>();
            Rigidbody rigidbody = other.gameObject.GetComponent<Rigidbody>();
            collider.isTrigger = true;
            rigidbody.useGravity = !rigidbody.useGravity;
            other.gameObject.transform.position = gameObject.transform.position + new Vector3(0.5f,0.4f,0f);
            
        }
        else
        {
            Collider collider1 = other.gameObject.GetComponent<Collider>();
            Rigidbody rigidbody1 = other.gameObject.GetComponent<Rigidbody>();
            if (collider1.isTrigger)
            {
                collider1.isTrigger = false;
            }

            if (rigidbody1.useGravity)
            {
                rigidbody1.useGravity = true;
            }
  
        }
            
    }

}
