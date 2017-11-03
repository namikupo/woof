using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ownerController : MonoBehaviour
{
    // Author: Eskild Middelboe
    // Function: The script controls the owner.
    // The script controls his actions, as well as the variables that controls his behavior.

    // Declaring the variables and gameObjects that are used.
    private int love;

    private int fetches;
    public bool depressed;
    public bool throwingDogToy;
    private bool fadeBool;
    private int endMoveTime;

    private GameObject doggoToy;
    private Rigidbody doggoToyRB;
    private GameObject player;
    public GameObject grabHand;
    private Vector3 boneSize;
    private Animator anim;

    [SerializeField]
    private mainMechanic mainMec;

    [SerializeField]
    private ParticleSystem hearts;

    [SerializeField]
    private imageFade imgFade;

    // Love needs to be accessed by the ChangeMood script, so that the color can change in accordance with the amount of items the player has delivered to the owner.
    public int Love
    {
        get
        {
            return love;
        }

        set
        {
            love = value;
        }
    }

    private void Start()
    {
        // Defining all the variables
        endMoveTime = 0;
        throwingDogToy = false;
        fadeBool = true;
        doggoToy = GameObject.FindGameObjectWithTag("dogToy");
        doggoToyRB = doggoToy.GetComponent<Rigidbody>();
        player = GameObject.Find("FPSController");
        GameObject ownerChar = GameObject.Find("OwnerMappedV4");
        anim = ownerChar.GetComponent<Animator>();
        grabHand = GameObject.Find("HandFollow1");
        anim.SetBool("gameOver", false);

        // This defines whether the owner is in his depressed or happy stage.
        // Used so the owner plays the right animations in the right scene.
        if (PlayerPrefs.GetInt("ownerSad") == 1)
        {
            anim.SetBool("animDepressed", true);
        }
        else
        {
            PlayerPrefs.SetInt("ownerSad", 0);
            anim.SetBool("animDepressed", false);
        }
    }

    // This is the method that makes the owner pick up the dog toy if the player delivers it.
    public void fetchQuest()
    {
        doggoToyRB.isKinematic = true;
        // The player must not be able to drag the toy around, else the model bugs out.
        mainMec.canDrag = false;
        // If this bool is not set to true, this function will be called for about 5 frames in succession, cutting the gameplay time considerably.
        throwingDogToy = true;
        // The reason behind finding the "HandFollow" object is because if you set the parent to the actual owners hand gameobject, the game freaks out.
        // Meaning that it breaks the game, engine and more.
        doggoToy.transform.SetParent(GameObject.Find("HandFollow1").transform);
        doggoToy.transform.position = (new Vector3(GameObject.Find("HandFollow1").transform.position.x + 0.00565f, GameObject.Find("HandFollow1").transform.position.y + 0.01159f, GameObject.Find("HandFollow1").transform.position.z + 0.00471f));
        doggoToy.transform.rotation = Quaternion.Euler(GameObject.Find("HandFollow1").transform.rotation.x + 90f, GameObject.Find("HandFollow1").transform.rotation.y - 30f, GameObject.Find("HandFollow1").transform.rotation.z);
        // This simply instatiates hearts from the owner's chest, at the right angle so they don't float downwards.
        Instantiate(hearts, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Quaternion.Euler(270f, 0f, 0f));
        fetches++;
        // Starts a chain of coroutines for animating the picking up and throwing animations.
        StartCoroutine(pickingUp());
    }

    // The owner then throws the dog toy
    private IEnumerator throwing()
    {
        // The bone is thrown forward, using Forcemode.Impulse to simulate throw-physics.
        // The Addforce force vector is a tiny bit random in power, this is to add a bit of variety in comparison to the toy being thrown in the same arc over and over again.
        yield return new WaitForSecondsRealtime(1.3f);
        doggoToy.transform.SetParent(null);
        doggoToyRB.isKinematic = false;
        doggoToyRB.useGravity = true;
        mainMec.canDrag = true;
        doggoToyRB.AddForce(this.transform.forward * Random.Range(10f, 15f), ForceMode.Impulse);
        throwingDogToy = false;
    }

    public void cheeredUp()
    {
        // This function gets called when the right object enters his outer sphere-collider
        // This is where the owner gets cheered up.
        Instantiate(hearts, new Vector3(this.transform.position.x, this.transform.position.y + 3, this.transform.position.z), Quaternion.Euler(270f, 0f, 0f));
        StartCoroutine(Petting());
        Love++;
    }

    private IEnumerator pickingUp()
    {
        // By waiting a bit, it matches the timing of the animation when the owner throws the bone.
        anim.SetBool("pickUp", true);
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(throwing());
        anim.SetBool("pickUp", false);
    }

    private IEnumerator Petting()
    {
        // This just makes the owner play the pet animation.
        anim.SetBool("pet", true);
        yield return new WaitForSecondsRealtime(0.7f);
        anim.SetBool("pet", false);
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
        // When the player is done playing fetch the scene transitions.
        // fadeBool is there so it doesn't get called multiple times but only once.
        if (fetches >= 6 && PlayerPrefs.GetInt("ownerSad") == 0 && fadeBool == true)
        {
            StartCoroutine(transitioning());
        }
        // When the owner has recieved enough love, he will become happy and the player reaches the end.
        if (Love == 6)
        {
            //Here goes the end.
            end();
            Love++;
        }
    }

    private IEnumerator transitioning()
    {
        // When transitioning, it sets the playerpref "ownerSad" to 1, making the owner play his depressed animations.
        fadeBool = false;
        anim.SetBool("animDepressed", true);
        PlayerPrefs.SetInt("ownerSad", 1);
        StartCoroutine(imgFade.fadeOut());
        // The waiting is for a more smooth fading transition. (It lets the fade play in full)
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(1);
    }

    // This function is called when the player has cheered up the owner.
    private void end()
    {
        mainMec.canMove = false;
        mainMec.canDrag = false;
        anim.SetBool("animDepressed", false);
        player.GetComponent<FirstPersonController>().gravityEnabled = false;
        player.transform.eulerAngles = new Vector3(0f, 0f, 0f);
        player.transform.SetParent(GameObject.Find("HeadFollow").transform);
        player.transform.position = (new Vector3(GameObject.Find("HeadFollow").transform.position.x + 2, player.transform.position.y, GameObject.Find("HeadFollow").transform.position.z));
        StartCoroutine(beginningOfEnd());
    }

    private IEnumerator beginningOfEnd()
    {
        yield return new WaitForSecondsRealtime(5.8f);
        StartCoroutine(movingToOwnerEnd());
    }

    private IEnumerator movingToOwnerEnd()
    {
        while (endMoveTime < 230)
        {
            endMoveTime++;
            player.transform.position = (new Vector3(player.transform.position.x - 0.01f, player.transform.position.y, player.transform.position.z));
            yield return new WaitForSecondsRealtime(0.025f);
            StartCoroutine(movingToOwnerEnd());
        }

        if (endMoveTime > 229)
        {
            StartCoroutine(ending());
        }
    }

    private IEnumerator ending()
    {
        StartCoroutine(imgFade.fadeOut());
        yield return new WaitForSecondsRealtime(4);
        PlayerPrefs.SetInt("ownerSad", 0);
        SceneManager.LoadScene(0);
    }
}