using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterial : MonoBehaviour {

    //Declared variables

    public Material[] material;
    public Renderer rend;

    public Color ColorMax;
    public Color ColorMin;
    public float timeChange;

    bool IsColorMax;
    float timer;

    //On startup, the script

	void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
	}

   
    //Collider that checks objects with the tag 'draggable', then stops all coroutines, sets the timer to 0 and calls the function 'ColorChange' 
    //void OnTriggerEnter (Collider collision)
    void Update ()
    {
        //if (collision.tag == "draggable")
        if (Input.GetKeyDown (KeyCode.Y))
        {
            StopAllCoroutines();
            timer = 0;
            StartCoroutine(ColorChange());
        }
    }

    IEnumerator ColorChange() //function that gradually changes the color overlay on an the object
    {
        IsColorMax = !IsColorMax;
        if(IsColorMax)
        {
            //This 'if' statement determines which color the object will become and or is, between the parameters ColorMax and ColorMin
            while (timer < timeChange)
            {
                //The Time.deltaTime; is used to tell how much time has passed on the timer and is referred to as 'timechange'
                timer += Time.deltaTime;
                //This line interperates the selected MaxColor and changes it gradually using 'Lerp' 
                rend.material.color = Color.Lerp(rend.material.color, ColorMax, timer / timeChange);
                //The process is ended in the same frame the color has reached 'ColorMax'
                yield return new WaitForEndOfFrame();
            }
            //The timer is promptly set to 0 and the rend.material.color is ColorMax
            timer = 0;
            rend.material.color = ColorMax;
        }
        //Otherwise (when the color is ColorMin) the same process happens but with the oter selected color 
        else
        {
            while (timer < timeChange)
            {
                timer += Time.deltaTime;
                rend.material.color = Color.Lerp(rend.material.color, ColorMin, timer / timeChange);
                yield return new WaitForEndOfFrame();
            }
            timer = 0;
            rend.material.color = ColorMin;
        }
    }
}

