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

    private int fetches;
    public bool depressed;

    private GameObject doggoToy;
    private Rigidbody doggoToyRB;

    [SerializeField]
    private mainMechanic mainMec;

    [SerializeField]
    private ParticleSystem hearts;

    private void Start()
    {
        doggoToy = GameObject.FindWithTag("dogToy");
        doggoToyRB = doggoToy.GetComponent<Rigidbody>();
        depressed = false;
        fetches = 0;
    }

    private void Awake()
    {
        // Initiate starting animation and loop.
    }

    // This is the method that makes the owner pick up the dog toy
    public void fetchQuest()
    {
        mainMec.canDrag = false;
        doggoToy.transform.position = (new Vector3(this.transform.position.x + 5f, this.transform.position.y + 8f, this.transform.position.z + 1f));
        doggoToyRB.isKinematic = true;
        StartCoroutine(throwing());
        fetches++;
    }

    // The owner then throws the dog toy
    private IEnumerator throwing()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        doggoToyRB.isKinematic = false;
        doggoToyRB.useGravity = true;
        mainMec.canDrag = true;
        doggoToyRB.AddRelativeForce(this.transform.forward * Random.Range(10f, 15f)s, ForceMode.Impulse);
    }

    public void cheeredUp()
    {
        // This function gets called when the right object enters his outer sphere-collider
        // This is where the owner gets cheered up.
        Instantiate(hearts, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Quaternion.Euler(270f, 0f, 0f));

        love++;
    }

    private void Update()
    {
        if (fetches >= 6)
        {
            // Insert method that makes the owner pick up the phone, and then get depressed afterwards.
            // For now it is an instant transition
            depressed = true;
        }

        // When the owner has recieved enough love, he will become happy and the player reaches the end.
        if (love == 6)
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