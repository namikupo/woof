using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ownerCheck : MonoBehaviour
{
    // Author: Eskild Middelboe
    // Function: This script checks the owner's surrounding collider for incoming objects.
    // If the the object is among a list of specific objects, it calls the ownerController cheeredUp function.
    // Utilizing a list of tags as a means to identify which objects are on the list, removing them once having called the function once.

    [SerializeField]
    private ownerController ownerCon;

    [SerializeField]
    private List<string> happyObjects = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (this.happyObjects.Contains(other.tag))
        {
            ownerCon.cheeredUp();
            happyObjects.Remove(other.tag);
        }
    }
}