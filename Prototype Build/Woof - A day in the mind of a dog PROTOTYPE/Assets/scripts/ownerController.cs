using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ownerController : MonoBehaviour
{
    // Author: Eskild Middelboe
    // Function: The script controls the owner.
    // The script controls his actions, as well as the variables that controls his behavior.

    // Define animation & points in the world the owner has to go to.
    // Defining the int variable for progression and which particlesystem that is used.
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
        Instantiate(hearts,  new Vector3(this.transform.position.x,this.transform.position.y + 3, this.transform.position.z), Quaternion.Euler(270f, 0f, 0f));

        love++;
    }

    private void Update()
    {
        // When the owner has recieved enough love, he will become happy and the player reaches the end.
        if (love == 7)
        {
            //Here goes the end/story progression
            end();
            love++;
        }
    }

    // This funct
    private void end()
    {
        //Here is the
    }
}