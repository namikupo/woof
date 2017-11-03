using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class vaseBreak : MonoBehaviour
{
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
    private AudioClip vaseSound;

    [SerializeField]
    private AudioSource aSource;

    private bool broken;

    private void Start()
    {
        broken = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "plane" && broken == false)
        {
            broken = true;
            aSource.PlayOneShot(vaseSound);
            anim.SetBool("gameOver", true);
            mainMec.canMove = false;
            fpsCon.gravityEnabled = false;
            //GameObject.Find("FPSController").transform.localRotation
            GameObject.Find("FPSController").transform.position = (new Vector3(GameObject.Find("Sofa").transform.position.x + 10, GameObject.Find("Sofa").transform.position.y + 2, GameObject.Find("Sofa").transform.position.z));
            GameObject.Find("FPSController").transform.SetParent(GameObject.Find("Sofa").transform);
            GameObject.Find("FPSController").transform.eulerAngles = new Vector3(0f, 90f, 0f);
            Instantiate(scaryLight, GameObject.Find("FPSController").transform.position, Quaternion.Euler(240, 90, 0));
            StartCoroutine(restarting());
        }
    }

    private IEnumerator restarting()
    {
        yield return new WaitForSecondsRealtime(2f);
        StartCoroutine(imgFade.fadeOut());
        yield return new WaitForSecondsRealtime(3f);
        SceneManager.LoadScene(1);
    }
}