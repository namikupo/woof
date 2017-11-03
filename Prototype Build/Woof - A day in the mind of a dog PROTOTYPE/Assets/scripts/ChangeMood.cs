using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMood : MonoBehaviour
{
    public Material[] material;
    public SkinnedMeshRenderer rend;

    public Color ExtremelySad;
    public Color Color1;
    public Color Color2;
    public Color Color3;
    public Color Color4;
    public Color Color5;
    public Color Color6;

    [SerializeField]
    private ownerController ownerCon;

    public float timeChange;

    private bool isExtremelySad;
    private float timer;

    private void Start()
    {
        rend = GetComponent<SkinnedMeshRenderer>();
        rend.enabled = true;
        //rend.sharedMaterial = material[0];
        isExtremelySad = true;
        rend.material.color = Color1;
    }

    private void Update()
    {
        /*if  (Input.GetKeyDown(KeyCode.U))
        {
            ownerCon.Love++;
        }
         */
        if (ownerCon.Love >= 1)
        {
            //StopAllCoroutines();
            timer = 0;
            StartCoroutine(ExtremelyDepressed());
        }
    }

    private IEnumerator ExtremelyDepressed() //function that gradually changes the color overlay on an the object
    {
        if (isExtremelySad && ownerCon.Love == 1)
        {
            //This 'if' statement determines which color the object will become and or is, between the parameters ColorMax and ColorMin
            while (timer < timeChange)
            {
                //The Time.deltaTime; is used to tell how much time has passed on the timer and is referred to as 'timechange'
                timer += Time.deltaTime;
                //This line interperates the selected MaxColor and changes it gradually using 'Lerp'
                rend.material.color = Color.Lerp(rend.material.color, Color1, timer / timeChange);
                //The process is ended in the same frame the color has reached 'ColorMax'
                yield return new WaitForEndOfFrame();
            }
            //The timer is promptly set to 0 and the rend.material.color is ColorMax
            timer = 0;
            rend.material.color = Color1;
        }

        //The following are multiple 'else if' statements that determine which color to change based on the amount of 'Love'
        else if (isExtremelySad && ownerCon.Love == 2)
        {
            while (timer < timeChange)
            {
                timer += Time.deltaTime;

                rend.material.color = Color.Lerp(rend.material.color, Color2, timer / timeChange);

                yield return new WaitForEndOfFrame();
                timer = 0;
            }

            timer = 0;
            rend.material.color = Color2;
        }
        else if (isExtremelySad && ownerCon.Love == 3)
        {
            while (timer < timeChange)
            {
                timer += Time.deltaTime;

                rend.material.color = Color.Lerp(rend.material.color, Color3, timer / timeChange);

                yield return new WaitForEndOfFrame();
                timer = 0;
            }

            timer = 0;
            rend.material.color = Color3;
        }
        else if (isExtremelySad && ownerCon.Love == 4)
        {
            while (timer < timeChange)
            {
                timer += Time.deltaTime;

                rend.material.color = Color.Lerp(rend.material.color, Color4, timer / timeChange);

                yield return new WaitForEndOfFrame();
                timer = 0;
            }

            timer = 0;
            rend.material.color = Color4;
        }
        else if (isExtremelySad && ownerCon.Love == 5)
        {
            while (timer < timeChange)
            {
                timer += Time.deltaTime;

                rend.material.color = Color.Lerp(rend.material.color, Color5, timer / timeChange);

                yield return new WaitForEndOfFrame();
                timer = 0;
            }

            timer = 0;
            rend.material.color = Color5;
        }
        else if (isExtremelySad && ownerCon.Love == 6)
        {
            while (timer < timeChange)
            {
                timer += Time.deltaTime;

                rend.material.color = Color.Lerp(rend.material.color, Color6, timer / timeChange);

                yield return new WaitForEndOfFrame();
                timer = 0;
            }
            timer = 0;
            rend.material.color = Color6;
        }
    }
}