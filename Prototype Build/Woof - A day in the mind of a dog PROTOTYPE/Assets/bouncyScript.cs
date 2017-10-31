using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bouncyScript : MonoBehaviour
{
    [SerializeField]
    private FirstPersonController fpsCon;

    // Author: Eskild
    // Function: To make the player bounce on the sofa

    /*  private void OnTriggerStay(Collider other)
      {
          if (other.tag == "Player")
          {
              fpsCon.JumpSpeed = (fpsCon.JumpSpeed * 133f);
          }
      }*/
}