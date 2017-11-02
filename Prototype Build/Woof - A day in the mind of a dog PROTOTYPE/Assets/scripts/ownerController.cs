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
    public bool throwingDogToy;
    private bool fadeBool;

    private GameObject doggoToy;
    private Rigidbody doggoToyRB;
    public GameObject grabHand;
    private Vector3 boneSize;

    private Animator anim;

    [SerializeField]
    private mainMechanic mainMec;

    [SerializeField]
    private ParticleSystem hearts;

    [SerializeField]
    private imageFade imgFade;

    private void Start()
    {
        throwingDogToy = false;
        fadeBool = true;
        doggoToy = GameObject.FindGameObjectWithTag("dogToy");
        boneSize = doggoToy.transform.lossyScale;
        doggoToyRB = doggoToy.GetComponent<Rigidbody>();
        GameObject ownerChar = GameObject.Find("OwnerMappedV4");
        anim = ownerChar.GetComponent<Animator>();
        grabHand = GameObject.Find("Palm.L");
        anim.SetBool("gameOver", false);

        if (PlayerPrefs.GetInt("ownerSad") == 1)
        {
            anim.SetBool("animDepressed", true);
        }
        else
        {
            anim.SetBool("animDepressed", false);
            PlayerPrefs.SetInt("ownerSad", 0);
        }
    }

    private void Awake()
    {
        // Initiate starting animation and loop.
    }

    // This is the method that makes the owner pick up the dog toy
    public void fetchQuest()
    {
        doggoToyRB.isKinematic = true;
        //doggoToyRB.useGravity = false;
        mainMec.canDrag = false;
        //throwingDogToy = true;
        //doggoToy.transform.position = (new Vector3(this.transform.position.x + 1f, this.transform.position.y + 4f, this.transform.position.z + 1f));
        //doggoToy.transform.SetParent(grabHand.transform, false);
        doggoToy.transform.SetParent(GameObject.Find("Palm.L").transform);
        doggoToy.transform.position = (new Vector3(GameObject.Find("Palm.L").transform.position.x, GameObject.Find("Palm.L").transform.position.y, GameObject.Find("Palm.L").transform.position.z));
        doggoToy.transform.rotation = GameObject.Find("Palm.L").transform.rotation;
        //doggoToy.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        //doggoToy.transform.localScale = boneSize;
        fetches++;
        StartCoroutine(pickingUp());
    }

    // The owner then throws the dog toy
    private IEnumerator throwing()
    {
        //this.transform.rotation = Quaternion.Euler(0, Random.Range(60, 240), 0);
        // anim.SetInteger("throw", 1);
        yield return new WaitForSecondsRealtime(1.5f);
        //doggoToy.transform.SetParent(null);
        doggoToy.transform.SetParent(null);
        //doggoToy.transform.localScale = boneSize;
        doggoToyRB.isKinematic = false;
        doggoToyRB.useGravity = true;
        mainMec.canDrag = true;
        doggoToyRB.AddForce(this.transform.forward * Random.Range(10f, 15f), ForceMode.Impulse);
        //this.transform.rotation = Quaternion.Euler(0, 180, 0);
        //  anim.SetInteger("throw", 0);
        throwingDogToy = false;
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
        anim.SetBool("pickUp", true);
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(throwing());
        anim.SetBool("pickUp", false);
    }

    private IEnumerator Petting()
    {
        anim.SetBool("pet", true);
        yield return new WaitForSecondsRealtime(0.7f);
        anim.SetBool("pet", false);
    }

    private void Update()
    {
        /* if (throwingDogToy == true)
         {
             //doggoToy.transform.SetParent(grabHand.transform);
             //doggoToy.transform.localPosition = (new Vector3(grabHand.transform.position.x - 0.2f, grabHand.transform.position.y + 0.15f, grabHand.transform.position.z + 0.2f));
             //doggoToy.transform.rotation = grabHand.transform.rotation;
         }*/

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
        anim.SetBool("animDepressed", true);
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