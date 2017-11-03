using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class imageFade : MonoBehaviour
{
    // Author: Eskild Middelboe
    // Function: Making a fade effect when transitioning scenes.

    // The image to fade in and out of.
    [SerializeField]
    private Image blackFade;

    // The image is spread over the entire canvas, covering all of the camera.

    public IEnumerator fadeIn()
    {
        // float i dictates the amount of time the fade happens. If you take Time.deltaTime and time it by 0.5, it will take twice as long for the fade to happen.
        for (float i = 2; i >= 0; i -= Time.deltaTime)

        {
            // The fade effect is achieved by adjusting the alpha of the color of the image.
            blackFade.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    public IEnumerator fadeOut()
    {
        // Here it is simply adding Time.deltaTime instead of subtracting it.
        for (float i = 0; i <= 3; i += Time.deltaTime)
        {
            blackFade.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }

    // This is used for the second game transition. The game designer asked that this one lasted a bit longer.
    public IEnumerator longFadeIn()
    {
        for (float i = 5; i >= 0; i -= Time.deltaTime)

        {
            blackFade.color = new Color(1, 1, 1, i);
            yield return null;
        }
    }
}