using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMechanic : MonoBehaviour
{
    // Objekt som skal indsættes når man interagere med objekt.
    [SerializeField]
    private GameObject sphere;

    [SerializeField]
    private GameObject trampoline;

    //Troels: Lyd der afspilles når objekt bliver indsat.
    [SerializeField]
    private AudioClip changeSound;

    //Troels: Lyd der afspilles når et objekt tages fat i.
    [SerializeField]
    private AudioClip biteSound;

    //Troels: Lyd der afspilles når et objekt gives slip på.
    [SerializeField]
    private AudioClip biteReleaseSound;

    //Troels: Denne AudioSource bruges til at afspille lydeffekterne.
    [SerializeField]
    private AudioSource aSource;

    public float volController;
    private bool dragSoundPlayed;

    // Forfatter: Eskild Middelboe & Troels Jensen
    // Metode: Spilleren skal kunne forvandle objekter, samt samle objekter op og flytte dem.
    // De individuelle objekter der kan trækkes skal defineres på forhånd.
    private void Start()
    {
        dragSoundPlayed = false;
        aSource = GetComponent<AudioSource>();
        volController = 1f;
    }

    // Scriptet tager det objekt man vil trække i, og sætter dens position i verdenen til at være lig ens egen.

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
            stopDraggingObject();
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && other.name == "Interactable object")
        {
            Destroy(other.gameObject);

            if (other.tag == ("interactableObject"))

            {
                Instantiate(sphere, other.transform.position, Quaternion.Euler(0f, 0f, 0f));
                aSource.PlayOneShot(changeSound, 1f);
            }

            Instantiate(sphere, other.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && other.name == "Interactable Static Object")
        {
            Destroy(other.gameObject);
            Instantiate(trampoline, other.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }
        else if (other.tag == "draggable")
        {
            //Der skal opsættes en seperat metode til at dragge hvert objekt (måske), derfor foreslås det at det ikke er så mange objekter der skal trækkes.
            //Troels: Derudover skal der startes en lyd som indikere at objektet bliver grebet fat i.

            // Der skal opsættes en seperat metode til at dragge hvert objekt (måske), derfor foreslås det at det ikke er så mange objekter der skal trækkes.
            // Dette script vil blive ryddet op senere.

            if (Input.GetKey(KeyCode.Mouse1))
            {
                dragObject();

                if (!dragSoundPlayed)
                {
                    aSource.PlayOneShot(biteSound, 1f);
                    dragSoundPlayed = true;
                }
            }

            //Troels: Når der ikke er grebet fat i objektet mere, indikeres der med en lyd at der er blevet givet slip.
            if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                aSource.PlayOneShot(biteReleaseSound, 1f);
                dragSoundPlayed = false;
            }
        }
    }

    private void stopDraggingObject()
    {
        Collider collider1 = GameObject.FindGameObjectWithTag("draggable").GetComponent<Collider>();
        Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("draggable").GetComponent<Rigidbody>();
        if (collider1.isTrigger == true)
        {
            collider1.isTrigger = false;
        }

        if (rigidbody1.useGravity == false)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true)
        {
            rigidbody1.isKinematic = false;
        }
    }

    private void dragObject()
    {
        GameObject dragdObject = GameObject.Find("Draggable object(Clone)");
        Collider collider = GameObject.FindGameObjectWithTag("draggable").GetComponent<Collider>();
        Rigidbody rigidbody = GameObject.FindGameObjectWithTag("draggable").GetComponent<Rigidbody>();
        ConstantForce forceObject = GameObject.FindGameObjectWithTag("draggable").GetComponent<ConstantForce>();
        collider.isTrigger = true;
        rigidbody.isKinematic = rigidbody.isKinematic = true;
        dragdObject.gameObject.transform.position = gameObject.transform.position;
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }
}