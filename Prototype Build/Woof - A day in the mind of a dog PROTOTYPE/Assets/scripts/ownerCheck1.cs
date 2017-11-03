using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ownerCheck1 : MonoBehaviour
{
    // Author: Eskild Middelboe
    // Function: This script checks the owner's surrounding collider for incoming objects.
    // If the the object is among a list of specific objects, it calls the ownerController cheeredUp function.
    // Utilizing a list of tags as a means to identify which objects are on the list, removing them once having called the function once.

    // Defining the foreign script, as well as the list.
    [SerializeField]
    private ownerController12 ownerCon;

    [SerializeField]
    private List<string> happyObjects = new List<string>();

    [SerializeField]
    private List<string> fetchObjects = new List<string>();

    //Whenever an object enters collider that is a triggercollider
    private void OnTriggerEnter(Collider other)
    {
        if (ownerCon.depressed == false)
        {
            if (this.fetchObjects.Contains(other.tag) && ownerCon.throwingDogToy == false)
            {
                ownerCon.fetchQuest();
            }
        }

        // If the the other object has a tag that is on the list
        if (ownerCon.depressed == true)
        {
            if (this.happyObjects.Contains(other.tag))
            {
                // Call the owner's response function and remove the tag from the list, preventing the object from getting a response twice.
                ownerCon.cheeredUp();
                happyObjects.Remove(other.tag);
            }
        }
    }
}