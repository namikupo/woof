using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    [SerializeField] private float dogSpeed;
    [SerializeField] private float dogJumpSpeed;
    [SerializeField] private mouseLook dogMouseLook;
    


    private Camera dogCamera;
    private Rigidbody dogRB;
    private int dogDirection;

	private void Start ()
    {
        dogCamera = Camera.main;
        dogRB = GetComponent<Rigidbody>();

    }
	
	private void Update ()
    {
        RotateView();
        moving();
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

        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A))
        {
            dogDirection = 5;
        }

        if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.S))
        {
            dogDirection = 6;
        }

        if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.D))
        {
            dogDirection = 7;
        }

        if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.W))
        {
            dogDirection = 8;
        }

        // Jump code
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            if (Physics.Raycast(transform.position, Vector3.down, 2f))
            {
                dogRB.AddForce(Vector3.up, ForceMode.Impulse);
            }

        }

        // Deciding what direction the player should go.
        switch (dogDirection)
        {
            case 1:
                {
                    dogRB.AddForce(Vector3.forward, ForceMode.VelocityChange);
                    break;
                }
            case 2:
                {
                    dogRB.AddForce(Vector3.left, ForceMode.VelocityChange);
                    break;
                }
            case 3:
                {
                    dogRB.AddForce(Vector3.back, ForceMode.VelocityChange);
                    break;
                }
            case 4:
                {
                    dogRB.AddForce(Vector3.right, ForceMode.VelocityChange);
                    break;
                }
            case 5:
                {
                    dogRB.AddForce(new Vector3(-1, 0, 1), ForceMode.VelocityChange);
                    break;
                }
            case 6:
                {
                    dogRB.AddForce(new Vector3(-1, 0, -1), ForceMode.VelocityChange);
                    break;
                }
            case 7:
                {
                    dogRB.AddForce(new Vector3(1, 0, -1),ForceMode.VelocityChange);
                    break;
                }
            case 8:
                {
                    dogRB.AddForce(new Vector3(1, 0, 1), ForceMode.VelocityChange);
                    break;
                }

        }
    }
}
