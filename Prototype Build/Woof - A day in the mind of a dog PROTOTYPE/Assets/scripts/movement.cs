using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float dogSpeed;
    [SerializeField] private float dogJumpSpeed;
    [SerializeField] private mouseLook dogMouseLook;
    


    private Camera dogCamera;
    private CharacterController dogController;
    private bool dogJump;


	private void Start ()
    {
        dogCamera = Camera.main;
        dogController = GetComponent<CharacterController>();

    }
	
	private void Update ()
    {
        RotateView();
	}

    private void FixedUpdate()
    {
        UpdateCameraPosition(dogSpeed);
    }

    private void UpdateCameraPosition(float dogSpeed)
    {
        Vector3 newCameraPosition;
        newCameraPosition = dogCamera.transform.localPosition;
        dogCamera.transform.localPosition = newCameraPosition;
    }

    private void RotateView()
    {
        dogMouseLook.LookRotation(transform, dogCamera);
    }
}
