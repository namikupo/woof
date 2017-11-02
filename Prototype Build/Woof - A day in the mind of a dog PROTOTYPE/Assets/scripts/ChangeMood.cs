using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMood : MonoBehaviour {

    public Material[] material;
    public Renderer rend;

    public Color ExtremelySad;
    public Color VerySad;
    public Color LittleSad;
    public float timeChange;

    bool isExtremelySad;
    float timer;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
        isExtremelySad = true;
    }


    //Collider that checks objects with the tag 'draggable', then stops all coroutines, sets the timer to 0 and calls the function 'ColorChange' 
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "draggable")
        {
            StopAllCoroutines();
            timer = 0;
            StartCoroutine(ExtremelyDepressed());
        }
    }

    IEnumerator ExtremelyDepressed() //function that gradually changes the color overlay on an the object
    {
        if (isExtremelySad)
        {
            //This 'if' statement determines which color the object will become and or is, between the parameters ColorMax and ColorMin
            while (timer < timeChange)
            {
                //The Time.deltaTime; is used to tell how much time has passed on the timer and is referred to as 'timechange'
                timer += Time.deltaTime;
                //This line interperates the selected MaxColor and changes it gradually using 'Lerp' 
                rend.material.color = Color.Lerp(rend.material.color, ExtremelySad, timer / timeChange);
                //The process is ended in the same frame the color has reached 'ColorMax'
                yield return new WaitForEndOfFrame();
            }
            //The timer is promptly set to 0 and the rend.material.color is ColorMax
            timer = 0;
            rend.material.color = ExtremelySad;
        }
    }
}
