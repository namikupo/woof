using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseLook : MonoBehaviour {

    public float XSensitivity = 1.5f;
    public float YSensitivity = 1.5f;
    public float MinimumX = -90F;
    public float MaximumX = 90F;
    public bool lockCursor = true;

    private Quaternion dogTargetRotation;
    private Quaternion dogCamTargetRotation;
    private bool cursorIsLocked = true;

	public void Init(Transform character, Transform camera)
    {
        dogTargetRotation = character.localRotation;
        dogCamTargetRotation = camera.localRotation;
	}
	
    public void LookRotation(Transform character, Camera camera)
    {
      //  float yRotation =
    }

}
