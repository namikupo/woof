using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainMechanic : MonoBehaviour
{
    // Forfatter: Eskild Middelboe & Troels Jensen
    // Metode: Spilleren skal kunne forvandle objekter, samt samle objekter op og flytte dem.
    // De individuelle objekter der kan trækkes skal defineres på forhånd.

    // Eskild: Scriptet er inddelt i flere stykker.
    // Først bliver alle objekterne og variablerne declaret.
    // I start metoden bliver variablerne defineret, samt bestemt om spilleren er i den første del af spillet eller den anden ved hjælp after PlayerPrefs.
    // Grunden til at playerprefs bliver brugt er at de beholder deres værdi igennem scenetransitions. Som et alternativ kunne man have et objekt som ville bestemme det
    // Som et alternativ kunne man have et objekt som ville bestemme det, som så ikke ville blive ødelagt når scene skifter.
    // Med playerprefs er der også chancen at spilleren bruger alt+f4 i stedet for at trykke escape, som ikke sletter PlayerPrefs. Dette kan ødelægge spillet.
    // Efter start-metoden kommer de andre metoder, som sørger for at spilleren kan samle objekter op samt tabe dem igen.
    // Til sidst er der en ubrugt metode, den er ikke blevet fjernet grundet frygt for at scriptet bryder sammen.
    // Til kontekst har der været problemer med unity-enginen og at den ikke har opføret sig planmæssigt, men i stedet har brudt sine egne regler.

    // Eskild har skrevet alt det der ikke involvere lyd.
    // Troels har skrevet for alt der har involveret lyd.

    // Eskild: Test Objekt som skal indsættes når man interagere med objekt.
    [SerializeField]
    private GameObject sphere;

    // Eskild: Test objekt for funktionalitet af objekter
    [SerializeField]
    private GameObject trampoline;

    //Troels: Lyd der afspilles når objekt bliver indsat.
    [SerializeField]
    private AudioClip changeSound;

    //Troels: Lyd der afspilles når et objekt tages fat i.

    [SerializeField]
    private AudioClip biteSound;

    [SerializeField]
    private AudioClip slipperSound;

    [SerializeField]
    private AudioClip dogToySound;

    [SerializeField]
    private AudioClip locketSound;

    [SerializeField]
    private AudioClip sockSound;

    [SerializeField]
    private AudioClip teddySound;

    [SerializeField]
    private AudioClip vaseSound;

    //Troels: Lyd der afspilles når et objekt gives slip på.
    [SerializeField]
    private AudioClip biteReleaseSound;

    //Troels: Denne AudioSource bruges til at afspille lydeffekterne.
    [SerializeField]
    private AudioSource aSource;

    [SerializeField]
    public FirstPersonController firPerCon;

    [SerializeField]
    private ownerController ownerCon;

    [SerializeField]
    private imageFade imgFade;

    private Camera dogCam;
    public float volController;
    private bool dragSoundPlayed;
    public bool canDrag;
    public bool canMove;

    private void Start()
    {
        if (PlayerPrefs.GetInt("ownerSad") == 1)
        {
            StartCoroutine(imgFade.longFadeIn());
        }
        else
        {
            StartCoroutine(imgFade.fadeIn());
        }
        dragSoundPlayed = false;
        aSource = GetComponent<AudioSource>();
        volController = 1f;
        canDrag = true;
        dogCam = Camera.main;
        canMove = true;
    }

    private void Update()
    {
        // Eskild: Spilleren kan flytte musen hurtigere end objekterne kan nå at følge med. For at objekter ikke ved et uheld flyver i luften sørger der for at de falder her.

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            stopDraggingSlipper();
            stopDraggingNewspaper();
            stopDraggingSock();
            stopDraggingDogToy();
            stopDraggingTeddy();
            stopDraggingLocket();
        }

        // Eskild: Spilleren kan ikke bevæge sig hvis canMove er falsk, her ændre den variablerne i det movementScript der er brugt.

        if (canMove == false)
        {
            firPerCon.WalkSpeed = 0f;
            firPerCon.RunSpeed = 0f;
            firPerCon.JumpSpeed = 0f;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Som et sikkerhedsnet hvis objekterne kommer uden for det objekts collider som mainMechanic sidder på, slippes de.

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
        // Eskild: OnTriggerStay bliver brugt da det er oplagt at lave systemet på den måde.
        // Hvis vi har et objekt som skal fungere som et "falsk" sigtekorn for spilleren, er det smart at chekke hver frame hvis der er et objekt inde i sigtekornet.

        // Hvis objektet inde i sigtekornet er et objekt som skal forandre sig.
        // other collideren er noget af det smarteste kodeværktøj i unity jeg kender til.
        if (Input.GetKeyDown(KeyCode.Mouse0) && other.name == "Interactable object")
        {
            // If the object is interactable, it destroys the object and spawning a new object at the old object's position

            Destroy(other.gameObject);

            if (other.tag == ("interactableObject"))

            {
                // Man kunne eventuelt også instantiate det andet objekt med det gamle objekts rotation.
                Instantiate(sphere, other.transform.position, Quaternion.Euler(0f, 0f, 0f));
                aSource.PlayOneShot(changeSound, 1f);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0) && other.name == "Interactable Static Object")
        {
            Destroy(other.gameObject);
            Instantiate(trampoline, other.transform.position, Quaternion.Euler(0f, 0f, 0f));
        }

        // Eskild: Der skal opsættes en seperat metode til at dragge hvert objekt (måske), derfor foreslås det at det ikke er så mange objekter der skal trækkes.
        // Der skal opsættes en seperat metode til at dragge hvert objekt, derfor foreslås det at det ikke er så mange objekter i spillet der skal trækkes.

        // Der bruges en switch her. I tidligere versioner blev der brugt et langt if-statement, men dette blev erstattet da det er foreslået at gøre sådan
        // i c# referencen. Koden ser også meget medre overskueligt ud.
        switch (other.tag)
        {
            // Ved at bruge en switch, er det nemt at bare tilføje en ekstra case.
            case "slipper":
                {
                    // Objektet kan kun trækkes rundt når en knap bliver trykket ned og canDrag er lig sand
                    if (Input.GetKey(KeyCode.Mouse1) && canDrag == true)
                    {
                        dragSlipper();

                        // Troels: Derudover skal der startes en lyd som indikere at objektet bliver grebet fat i.
                        if (!dragSoundPlayed)
                        {
                            aSource.PlayOneShot(slipperSound, 1f);
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
                            aSource.PlayOneShot(sockSound, 1f);
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
                            aSource.PlayOneShot(dogToySound, 1f);
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
                            aSource.PlayOneShot(teddySound, 1f);
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
                            aSource.PlayOneShot(locketSound, 1f);
                            dragSoundPlayed = true;
                        }
                    }
                    return;
                }
            case "vase":
                {
                    // Spilleren kan ikke bevæger sig og dør.
                    // Dette system blev skiftet ud, og er ikke brugt.
                    if (Input.GetKeyDown(KeyCode.Mouse1))
                    {
                        other.transform.rotation = Quaternion.Euler(0, 0, 90);
                        canMove = false;
                        gameOver();
                    }
                    return;
                }
        }
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

    // Et eksempel på en metode der bliver kaldt når man ikke trækker et objekt mere.
    private void stopDraggingSlipper()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.Find("Slipper").GetComponent<Rigidbody>();
        // Objektet bliver flyttet tilbage til layeret hvor det kollidere med spilleren.
        rigidbody1.gameObject.layer = 0;

        // Der bliver brugt selektionsstrukturer her fordi, at mens der blev testet om hvorvidt objekterne skulle ikke bruge tyngdekraft eller sættes til at være kinematic, -
        //er systemet fleksibelt nok til at justere sig til hver mulighed, samt den endelige løsning.
        if (rigidbody1.useGravity == false)
        {
            rigidbody1.useGravity = true;
        }

        if (rigidbody1.isKinematic == true)
        {
            rigidbody1.isKinematic = false;
        }
    }

    // Et eksempel på en metode der bliver kaldt når man trækker et objekt
    private void dragSlipper()
    {
        // Objekter der trækkes i findes i sceenen..
        GameObject dragdObject = GameObject.Find("Slipper");
        Rigidbody rigidbody = GameObject.Find("Slipper").GetComponent<Rigidbody>();
        // Objekter sættes på et layer som ikke collidere med spilleren. Dette er sådan at når spilleren kigger langt nok ned med et højt objekt, skydes spilleren ikke i været.
        dragdObject.layer = 8;
        // Rigidbody'en flyttes i stedet for at bare ændre dens position. Dette er sådan at der er en hvis form for kollision med omgivelserne.
        rigidbody.position = (new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        // Hvis tyngdekraft stadig var slået til, ville en kræften hurtigt bygge op desto længere spilleren holder et objekt i luften.
        rigidbody.useGravity = false;
    }

    private void dragNewspaper()
    {
        GameObject dragdObject = GameObject.Find("News");
        Rigidbody rigidbody = GameObject.Find("News").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingNewspaper()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.Find("News").GetComponent<Rigidbody>();
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
        GameObject dragdObject = GameObject.Find("MissingSock");
        Rigidbody rigidbody = GameObject.Find("MissingSock").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingSock()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.Find("MissingSock").GetComponent<Rigidbody>();
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
        GameObject dragdObject = GameObject.Find("DoggyToy");
        Rigidbody rigidbody = GameObject.Find("DoggyToy").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    public void stopDraggingDogToy()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.Find("DoggyToy").GetComponent<Rigidbody>();
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
        GameObject dragdObject = GameObject.Find("Teddy");
        Rigidbody rigidbody = GameObject.Find("Teddy").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingTeddy()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;

        Rigidbody rigidbody1 = GameObject.Find("Teddy").GetComponent<Rigidbody>();
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
        GameObject dragdObject = GameObject.Find("Locket");
        Rigidbody rigidbody = GameObject.Find("Locket").GetComponent<Rigidbody>();
        dragdObject.layer = 8;
        rigidbody.position = (new Vector3(gameObject.transform.position.x, (gameObject.transform.position.y - 0.2f), gameObject.transform.position.z));
        dragdObject.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rigidbody.useGravity = false;
    }

    private void stopDraggingLocket()
    {
        aSource.PlayOneShot(biteReleaseSound, 1f);
        dragSoundPlayed = false;
        Rigidbody rigidbody1 = GameObject.Find("Locket").GetComponent<Rigidbody>();
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

    // Denne metoder er ikke brugt.
    public void gameOver()
    {
        ownerCon.depressed = false;
    }
}