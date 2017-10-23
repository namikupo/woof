using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float dogSpeed;
    [SerializeField] private float dogJumpSpeed;
    [SerializeField] private MouseLook dogMouseLook;

    private Camera dogCamera;
    private Rigidbody dogRB;
    private int dogDirection;

    public float DogJumpSpeed
    {
        get
        {
            return dogJumpSpeed;
        }

        set
        {
            dogJumpSpeed = value;
        }
    }

    private void Start()
    {
        dogCamera = Camera.main;
        dogRB = GetComponent<Rigidbody>();
        dogMouseLook.Init(transform, dogCamera.transform);
    }

    private void Update()
    {
        RotateView();
        moving();
    }

    private void FixedUpdate()
    {
        UpdateCameraPosition(dogSpeed);
        dogMouseLook.UpdateCursorLock();
    }

    private void UpdateCameraPosition(float dogSpeed)
    {
        Vector3 newCameraPosition;
        newCameraPosition = dogCamera.transform.localPosition;
        dogCamera.transform.localPosition = newCameraPosition;
    }

    private void RotateView()
    {
        dogMouseLook.LookRotation(transform, dogCamera.transform);
    }

    private void moving()
    {
        // Checking which combination of keys are held down.
        if (Input.GetKeyDown(KeyCode.W))
        {
            dogDirection = 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            dogDirection = 2;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            dogDirection = 3;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            dogDirection = 4;
        }

        if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.A))
        {
            dogDirection = 5;
        }

        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
        {
            dogDirection = 6;
        }

        if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.D))
        {
            dogDirection = 7;
        }

        if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
        {
            dogDirection = 8;
        }

        if (!Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
        {
            // dogRB.AddForce = new Vector3(0, 0, 0);
        }

        // Jump code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Physics.Raycast(transform.position, Vector3.down, 1.05f))
            {
                dogRB.AddForce(Vector3.up * DogJumpSpeed, ForceMode.Impulse);
            }
        }

        // Deciding what direction the player should go.
        switch (dogDirection)
        {
            case 1:
                {
                    dogRB.AddRelativeForce(Vector3.forward * dogSpeed, ForceMode.Impulse);
                    break;
                }
            case 2:
                {
                    dogRB.AddRelativeForce(Vector3.left * dogSpeed, ForceMode.Impulse);
                    break;
                }
            case 3:
                {
                    dogRB.AddRelativeForce(Vector3.back * dogSpeed, ForceMode.Impulse);
                    break;
                }
            case 4:
                {
                    dogRB.AddRelativeForce(Vector3.right * dogSpeed, ForceMode.Impulse);
                    break;
                }
            case 5:
                {
                    dogRB.AddRelativeForce(new Vector3(-1, 0, 1) * dogSpeed, ForceMode.Impulse);
                    break;
                }
            case 6:
                {
                    dogRB.AddRelativeForce(new Vector3(-1, 0, -1) * dogSpeed, ForceMode.Impulse);
                    break;
                }
            case 7:
                {
                    dogRB.AddRelativeForce(new Vector3(1, 0, -1) * dogSpeed, ForceMode.Impulse);
                    break;
                }
            case 8:
                {
                    dogRB.AddRelativeForce(new Vector3(1, 0, 1) * dogSpeed, ForceMode.Impulse);
                    break;
                }
        }
    }
}