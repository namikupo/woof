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
    public bool canDrag;

    // Forfatter: Eskild Middelboe & Troels Jensen
    // Metode: Spilleren skal kunne forvandle objekter, samt samle objekter op og flytte dem.
    // De individuelle objekter der kan trækkes skal defineres på forhånd.
    private void Start()
    {
        dragSoundPlayed = false;
        aSource = GetComponent<AudioSource>();
        volController = 1f;
        canDrag = true;
    }

    private void Update()
    {
        // This if statement is here, because the player can move the mouse faster than it drags the object.
        // If the object detaches from the object, it can this way still become normal instead of bugging out.

        /* if (Input.GetKeyUp(KeyCode.Mouse1))
         {
             stopDraggingObject();
             stopDraggingSlipper();
             stopDraggingNewspaper();
             stopDraggingSock();
             stopDraggingDogToy();
             stopDraggingTeddy();
             stopDraggingLocket();
         }*/
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "slipper")
            stopDraggingSlipper();
        else if (other.tag == "newspaper")
            stopDraggingNewspaper();
        else if (other.tag == "sock")
            stopDraggingSock();
        else if (other.tag == "dogToy")
            stopDraggingDogToy();
        else if (other.tag == "teddy")
            stopDraggingTeddy();
        else if (other.tag == "locket")
            stopDraggingLocket();
    }

    private void OnTriggerStay(Collider other)
    {
        // OnTriggerStay means that every frame that an object with a triggercollider is inside this collider, the OnTriggerStay is called.
        // This is used to check whether the Crosshair object is inside an object that the player can either interact with or drag around.

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            stopDraggingSlipper();
            stopDraggingNewspaper();
            stopDraggingSock();
            stopDraggingDogToy();
            stopDraggingTeddy();
            stopDraggingLocket();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && other.name == "Interactable object")
        {
            // If the object is interactable, it destroys the object and spawning a new object at the old object's position

            Destroy(other.gameObject);

            if (other.tag == ("interactableObject"))

            {
                Instantiate(sphere, other.transform.position, Quaternion.Euler(0f, 0f, 0f));
                aSource.PlayOneShot(changeSound, 1f);
            }

            Instantiate(sphere, other.transform.position, Quaternion.Euler(0f, 0f, 0f));
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

        //Der skal opsættes en seperat metode til at dragge hvert objekt (måske), derfor foreslås det at det ikke er så mange objekter der skal trækkes.
        //Troels: Derudover skal der startes en lyd som indikere at objektet bliver grebet fat i.

        // Der skal opsættes en seperat metode til at dragge hvert objekt (måske), derfor foreslås det at det ikke er så mange objekter der skal trækkes.
        // Dette script vil blive skrevet om på et senere tidspunkt.

        // Dette script vil blive ryddet op senere.

        switch (other.tag)
        {
            case "slipper":
                {
                    if (Input.GetKey(KeyCode.Mouse1) && canDrag == true)
                    {
                        dragSlipper();

                        if (!dragSoundPlayed)
                        {
                            aSource.PlayOneShot(biteSound, 1f);
                            dragSoundPlayed = true;
                        }
                    }

                    return;
                }
            case "newspaper":
                {
                    if (Input.GetKey(KeyCode.Mouse1) && canDrag == true)
                    {
                        dragNewspaper();

                        if (!dragSoundPlayed)
                        {
                            aSource.PlayOneShot(biteSound, 1f);
                            dragSoundPlayed = true;
                        }
                    }
                    return;
                }
            case "sock":
                {
                    if (Input.GetKey(KeyCode.Mouse1) && canDrag == true)
                    {
                        dragSock();

                        if (!dragSoundPlayed)
                        {
                            aSource.PlayOneShot(biteSound, 1f);
                            dragSoundPlayed = true;
                        }
                    }
                    return;
                }
            case "dogToy":
                {
                    if (Input.GetKey(KeyCode.Mouse1) && canDrag == true)
                    {
                        dragDogToy();

                        if (!dragSoundPlayed)
                        {
                            aSource.PlayOneShot(biteSound, 1f);
                            dragSoundPlayed = true;
                        }
                    }
                    return;
                }
            case "teddy":
                {
                    if (Input.GetKey(KeyCode.Mouse1) && canDrag == true)
                    {
                        dragTeddy();

                        if (!dragSoundPlayed)
                        {
                            aSource.PlayOneShot(biteSound, 1f);
                            dragSoundPlayed = true;
                        }
                    }
                    return;
                }
            case "locket":
                {
                    if (Input.GetKey(KeyCode.Mouse1) && canDrag == true)
                    {
                        dragLocket();

                        if (!dragSoundPlayed)
                        {
                            aSource.PlayOneShot(biteSound, 1f);
                            dragSoundPlayed = true;
                        }
                    }
                    return;
                }
        }

        /*
        if (Input.GetKey(KeyCode.Mouse1))
        {
            dragObject();

            if (!dragSoundPlayed)
            {
                aSource.PlayOneShot(biteSound, 1f);
                dragSoundPlayed = true;
            }
        }
    }
    else if (other.tag == "slipper")
    {
        if (Input.GetKey(KeyCode.Mouse1) && dragging == 1)
        {
            dragSlipper();

            if (!dragSoundPlayed)
            {
                aSource.PlayOneShot(biteSound, 1f);
                dragSoundPlayed = true;
            }
        }
    }
    else if (other.tag == "newspaper")
    {
        if (Input.GetKey(KeyCode.Mouse1) && dragging == 2)
        {
            dragNewspaper();

            if (!dragSoundPlayed)
            {
                aSource.PlayOneShot(biteSound, 1f);
                dragSoundPlayed = true;
            }
        }
    }
    else if (other.tag == "sock")
    {
        if (Input.GetKey(KeyCode.Mouse1) && dragging == 3)
        {
            dragSock();

            if (!dragSoundPlayed)
            {
                aSource.PlayOneShot(biteSound, 1f);
                dragSoundPlayed = true;
            }
        }
    }
    else if (other.tag == "dogToy")
    {
        if (Input.GetKey(KeyCode.Mouse1) && dragging == 4)
        {
            dragDogToy();

            if (!dragSoundPlayed)
            {
                aSource.PlayOneShot(biteSound, 1f);
                dragSoundPlayed = true;
            }
        }
    }
    else if (other.tag == "teddy")
    {
        if (Input.GetKey(KeyCode.Mouse1) && dragging == 5)
        {
            dragTeddy();

            if (!dragSoundPlayed)
            {
                aSource.PlayOneShot(biteSound, 1f);
                dragSoundPlayed = true;
            }
        }
    }
    else if (other.tag == "locket")
    {
        if (Input.GetKey(KeyCode.Mouse1) && dragging == 6)
        {
            dragLocket();

            if (!dragSoundPlayed)
            {
                aSource.PlayOneShot(biteSound, 1f);
                dragSoundPlayed = true;
            }
        }*/
    }

    private void stopDraggingObject()
    {
        //Troels: Når der ikke er grebet fat i objektet mere, indikeres der med en lyd at der er blevet givet slip.
        /*    if (Input.GetKeyUp(KeyCode.Mouse1))
            {
                aSource.PlayOneShot(biteReleaseSound, 1f);
                dragSoundPlayed = false;
            }

            Collider collider1 = GameObject.FindGameObjectWithTag("draggable").GetComponent<Collider>();
            Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("draggable").GetComponent<Rigidbody>();

            if (rigidbody1.useGravity == false)
            {
                rigidbody1.useGravity = true;
            }

            if (rigidbody1.isKinematic == true)
            {
                rigidbody1.isKinematic = false;
            }*/
    }

    private void dragObject()
    {
        /*      // Scriptet tager det objekt man vil trække i, og sætter dens position i verdenen til at være lig ens egen.

              GameObject dragdObject = GameObject.Find("Draggable object(Clone)");
              Collider collider = GameObject.FindGameObjectWithTag("draggable").GetComponent<Collider>();
              Rigidbody rigidbody = GameObject.FindGameObjectWithTag("draggable").GetComponent<Rigidbody>();
              ConstantForce forceObject = GameObject.FindGameObjectWithTag("draggable").GetComponent<ConstantForce>();
              collider.isTrigger = true;
              rigidbody.isKinematic = rigidbody.isKinematic = true;
              dragdObject.gameObject.transform.position = gameObject.transform.position;
              dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
              rigidbody.useGravity = false;*/
    }

    private void stopDraggingSlipper()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("slipper").GetComponent<Rigidbody>();
        rigidbody1.gameObject.layer = 0;
        if (rigidbody1.useGravity == false)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true)
        {
            rigidbody1.isKinematic = false;
        }
    }

    private void dragSlipper()
    {
        GameObject dragdObject = GameObject.FindGameObjectWithTag("slipper");
        Rigidbody rigidbody = GameObject.FindGameObjectWithTag("slipper").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void dragNewspaper()
    {
        GameObject dragdObject = GameObject.FindGameObjectWithTag("newspaper");
        Rigidbody rigidbody = GameObject.FindGameObjectWithTag("newspaper").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingNewspaper()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("newspaper").GetComponent<Rigidbody>();
        rigidbody1.gameObject.layer = 0;
        if (rigidbody1.useGravity == false)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true)
        {
            rigidbody1.isKinematic = false;
        }
    }

    private void dragSock()
    {
        GameObject dragdObject = GameObject.FindGameObjectWithTag("sock");
        Rigidbody rigidbody = GameObject.FindGameObjectWithTag("sock").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingSock()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("sock").GetComponent<Rigidbody>();
        rigidbody1.gameObject.layer = 0;
        if (rigidbody1.useGravity == false)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true)
        {
            rigidbody1.isKinematic = false;
        }
    }

    private void dragDogToy()
    {
        GameObject dragdObject = GameObject.FindGameObjectWithTag("dogToy");
        Rigidbody rigidbody = GameObject.FindGameObjectWithTag("dogToy").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
    }

    public void stopDraggingDogToy()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("dogToy").GetComponent<Rigidbody>();
        rigidbody1.gameObject.layer = 0;
        if (rigidbody1.useGravity == false && canDrag == true)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true && canDrag == true)
        {
            rigidbody1.isKinematic = false;
        }
    }

    private void dragTeddy()
    {
        GameObject dragdObject = GameObject.FindGameObjectWithTag("teddy");
        Rigidbody rigidbody = GameObject.FindGameObjectWithTag("teddy").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingTeddy()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("teddy").GetComponent<Rigidbody>();
        rigidbody1.gameObject.layer = 0;
        if (rigidbody1.useGravity == false)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true)
        {
            rigidbody1.isKinematic = false;
        }
    }

    private void dragLocket()
    {
        GameObject dragdObject = GameObject.FindGameObjectWithTag("locket");
        Rigidbody rigidbody = GameObject.FindGameObjectWithTag("locket").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingLocket()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;
        Rigidbody rigidbody1 = GameObject.FindGameObjectWithTag("locket").GetComponent<Rigidbody>();
        rigidbody1.gameObject.layer = 0;
        if (rigidbody1.useGravity == false)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true)
        {
            rigidbody1.isKinematic = false;
        }
    }
}