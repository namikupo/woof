using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour {

    Transform Hudson;

    void Start () {
        Hudson = GameObject.FindGameObjectWithTag("Player").transform;
    }
    // Code checking the distance between the player (Hudson) and objects with this script (transform.position)
    void Distance ()
    {
        float f = Vector3.Distance(transform.position, Hudson.position);
    }
}
