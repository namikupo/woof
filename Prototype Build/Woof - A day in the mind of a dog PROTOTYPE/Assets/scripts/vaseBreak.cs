using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vaseBreak : MonoBehaviour
{
    // Author: Eskild Middelboe
    // Function: Making the vase "break" when the trigger-collider at the top hits the ground.
    // This includes teleporting the player in front of the owner, so that they will see the scary animation.

    // Declaring the different assets needed to make the script work.
    [SerializeField]
    private imageFade imgFade;

    [SerializeField]
    private FirstPersonController fpsCon;

    [SerializeField]
    private GameObject scaryLight;

    [SerializeField]
    private mainMechanic mainMec;

    [SerializeField]
    private Animator anim;

    [SerializeField]
    private AudioClip spookSound;

    [SerializeField]
    private AudioClip vaseSound;

    [SerializeField]
    private AudioSource aSource;

    private bool broken;

    // Script starts out by setting broken to false. This is so the OnTriggerEnter if-statement only gets called once.
    private void Start()
    {
        broken = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        // The floor has the tag "plane", and the collider is situated at the top of the vase, making it so the player has to tilt the vase over for this if-statement to be called.
        if (other.tag == "plane" && broken == false)
        {
            broken = true;
            // The sound of the vase gets played
            aSource.PlayOneShot(vaseSound);
            // The player also cannot move, so that the entire focus is put on the scene.
            mainMec.canMove = false;
            // If gravity was still enabled, the player would shift through the floor.
            fpsCon.gravityEnabled = false;
            // One could define the player and Sofa object in advance, which would make for cleaner code.
            // However, when testing systems, having to declare and define a new object instead of simply testing whether it works is tiresome, and not time effecient.
            // On an interesting note, if you simply set the object's rotation, it will add the quaternion.euler to the current rotation.
            // Meaning if you want to set a fixed angle you have to use eulerAngles
            GameObject.Find("FPSController").transform.position = (new Vector3(GameObject.Find("Sofa").transform.position.x + 10, GameObject.Find("Sofa").transform.position.y + 2, GameObject.Find("Sofa").transform.position.z));
            GameObject.Find("FPSController").transform.SetParent(GameObject.Find("Sofa").transform);
            GameObject.Find("FPSController").transform.eulerAngles = new Vector3(0f, 90f, 0f);
            // Instantiating the light at the player's position at a certain angle, so that one can see the owner.
            Instantiate(scaryLight, GameObject.Find("FPSController").transform.position, Quaternion.Euler(240, 90, 0));
            // The animation is delayed so the player can adjust to the change in surroundings.
            StartCoroutine(ownerScary());
        }
    }

    private IEnumerator ownerScary()
    {
        aSource.PlayOneShot(spookSound);
        yield return new WaitForSecondsRealtime(4.2f);
        anim.SetBool("gameOver", true);
        // The game needs to restart to the beginning of the level.
        StartCoroutine(restarting());
    }

    private IEnumerator restarting()
    {
        // The wait functions are so the animation for the owner are played in full, as well as the transition back to the start of the level isn't too sudden.
        // On a note, you cannot start coroutines like normal functions. This has been a playing factor that was easily forgotten and reason to much frustration.
        yield return new WaitForSecondsRealtime(2.2f);
        StartCoroutine(imgFade.fadeOut());
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(1);
    }
}