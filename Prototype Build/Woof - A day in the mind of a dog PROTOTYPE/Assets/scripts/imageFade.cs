using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class imageFade : MonoBehaviour
{
    [SerializeField]
    private Image blackFade;

    public IEnumerator fadeIn()
    {
        for (float i = 5; i >= 0; i -= Time.deltaTime)

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
}