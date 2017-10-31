using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class interactionScript : MonoBehaviour
{
    [SerializeField]
    private CharacterController dogController;

    private Vector3 jumpVector;

    private FirstPersonController FirstPersonController;

    private void Start()
    {
        dogController = GetComponent<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == ("trampoline"))
        {
            jumpVector = new Vector3(0, 1, 0);
            jumpVector = transform.TransformDirection(jumpVector);
            jumpVector.y = jumpVector.y * FirstPersonController.JumpSpeed;
            dogController.Move(jumpVector);
        }
    }

    private void Update()
    {
        jumpVector.y -= 20f * Time.deltaTime;
    }

    private void jump()
    {
    }
}