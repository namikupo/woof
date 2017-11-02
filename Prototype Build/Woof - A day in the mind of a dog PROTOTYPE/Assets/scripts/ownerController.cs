using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool fadeBool;

    private GameObject doggoToy;
    private Rigidbody doggoToyRB;

    private Animator anim;

    [SerializeField]
    private mainMechanic mainMec;

    [SerializeField]
    private ParticleSystem hearts;

    [SerializeField]
    private imageFade imgFade;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ownerSad") == 1)
        {
        }
        else
        {
            PlayerPrefs.SetInt("ownerSad", 0);
        }

        fadeBool = true;
        doggoToy = GameObject.FindWithTag("dogToy");
        doggoToyRB = doggoToy.GetComponent<Rigidbody>();
        GameObject ownerChar = GameObject.Find("SittingOwner");
        anim = ownerChar.GetComponent<Animator>();
    }

    private void Awake()
    {
        // Initiate starting animation and loop.
    }

    // This is the method that makes the owner pick up the dog toy
    public void fetchQuest()
    {
        mainMec.canDrag = false;
        doggoToyRB.isKinematic = true;
        //doggoToy.transform.position = (new Vector3(this.transform.position.x + 1f, this.transform.position.y + 4f, this.transform.position.z + 1f));
        doggoToy.transform.SetParent(GameObject.Find("Palm.L").transform);
        doggoToy.transform.position = (new Vector3(GameObject.Find("Palm.L").transform.position.x, GameObject.Find("Palm.L").transform.position.y, GameObject.Find("Palm.L").transform.position.z));
        doggoToy.transform.rotation = GameObject.Find("Palm.L").transform.rotation;

        fetches++;
        StartCoroutine(pickingUp());
    }

    // The owner then throws the dog toy
    private IEnumerator throwing()
    {
        //this.transform.rotation = Quaternion.Euler(0, Random.Range(60, 240), 0);
        anim.SetInteger("throw", 1);
        yield return new WaitForSecondsRealtime(1f);
        doggoToy.transform.SetParent(null);
        doggoToyRB.isKinematic = false;
        doggoToyRB.useGravity = true;
        mainMec.canDrag = true;
        doggoToyRB.AddRelativeForce(this.transform.forward * Random.Range(10f, 15f), ForceMode.Impulse);
        //this.transform.rotation = Quaternion.Euler(0, 180, 0);
        anim.SetInteger("throw", 0);
    }

    public void cheeredUp()
    {
        // This function gets called when the right object enters his outer sphere-collider
        // This is where the owner gets cheered up.
        Instantiate(hearts, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Quaternion.Euler(270f, 0f, 0f));
        StartCoroutine(Petting());
        love++;
    }

    private IEnumerator pickingUp()
    {
        anim.SetInteger("pickUp", 1);
        yield return new WaitForSecondsRealtime(1.7f);
        StartCoroutine(throwing());
        anim.SetInteger("pickUp", 0);
    }

    private IEnumerator Petting()
    {
        anim.SetInteger("pet", 1);
        yield return new WaitForSecondsRealtime(0.7f);
        anim.SetInteger("pet", 0);
    }

    private void Update()
    {
        if (PlayerPrefs.GetInt("ownerSad") == 1)
        {
            depressed = true;
        }

        if (PlayerPrefs.GetInt("ownerSad") == 0)
        {
            depressed = false;
        }

        if (fetches >= 6 && PlayerPrefs.GetInt("ownerSad") == 0 && fadeBool == true)
        {
            // Insert method that makes the owner pick up the phone, and then get depressed afterwards.
            // For now it is an instant transition

            Debug.Log("Test");
            StartCoroutine(transitioning());
        }
        // When the owner has recieved enough love, he will become happy and the player reaches the end.
        if (love == 6)
        {
            //Here goes the end/story progression
            end();
            love++;
        }
    }

    private IEnumerator transitioning()
    {
        fadeBool = false;
        Debug.Log("Testing");
        PlayerPrefs.SetInt("ownerSad", 1);
        StartCoroutine(imgFade.fadeOut());
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(1);
    }

    // This funct
    private void end()
    {
        //Here is the
    }
}