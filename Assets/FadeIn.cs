using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{

    private void Start()
    {
        StartCoroutine(FadeyFade());
    }

    IEnumerator FadeyFade()
    {
        float fadeTime = 2.0f;

        Text t = GetComponent<Text>();
        
        for(float timer = 0.0f; timer < fadeTime; timer += Time.deltaTime)
        {
            yield return null;

            float lerpFactor = timer / fadeTime;

            Color c = t.color;
            c.a = Mathf.Lerp(0.0f, 1.0f, lerpFactor * 2);
            t.color = c;
        }
    }
}
