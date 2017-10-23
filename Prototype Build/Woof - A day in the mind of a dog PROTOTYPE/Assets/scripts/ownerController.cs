using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ownerController : MonoBehaviour
{
    // Author: Eskild Middelboe
    // Function: The script controls the owner.
    // The script controls his actions, as well as the variables that controls his behavior.

    // Define animation & points in the world the owner has to go to.
    private int love;

    [SerializeField]
    private ParticleSystem hearts;

    private void Awake()
    {
        // Initiate starting animation and loop.
    }

    public void cheeredUp()
    {
        // This function gets called when the right object enters his outer sphere-collider
        // This is where the owner gets cheered up.
        Instantiate(hearts, this.transform.position, Quaternion.Euler(270f, 0f, 0f));

        love++;
    }

    private void Update()
    {
        if (love >= 7)
        {
            //Here goes the end/story progression
        }
    }
}