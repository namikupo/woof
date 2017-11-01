using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class imageFade : MonoBehaviour
{
    [SerializeField]
    private Image blackFade;

    // Use this for initialization
    private void Start()
    {
    }

    public IEnumerator fadeIn()
    {
        for (float i = 3; i >= 0; i -= Time.deltaTime)

        {
            blackFade.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public IEnumerator fadeOut()
    {
        for (float i = 0; i <= 3; i += Time.deltaTime)
        {
            blackFade.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    // Update is called once per frame
    private void Update()
    {
    }
}