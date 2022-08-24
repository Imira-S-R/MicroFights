using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class panelFader : MonoBehaviour
{
    private bool mFaded = false;
    public float Duration = 0.4f;

    public void Fade ()
    {
        var canvGroup = GetComponent<CanvasGroup>();
        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));
        mFaded = !mFaded;
    }

    public IEnumerator DoFade(CanvasGroup canvasGroup, float start, float end)
    {
        float counter = 0f;
        while (counter < Duration)
        {
            counter += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }
}
