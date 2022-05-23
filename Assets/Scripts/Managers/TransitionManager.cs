using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    private float fadeDuration;
    private float topClose;
    private float topOpen;
    private float bottomClose;
    private float bottomOpen;
    private void Awake()
    {
        fadeDuration = 1.2f;
        bottomClose= -2.35f;
        topOpen = 6.88f;
        topClose = 2.2f;
        bottomOpen = -6.19f;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            top.position = new (0, Mathf.Lerp(topClose, topOpen, elapsedTime / fadeDuration), 0);
            bottom.position= new(0, Mathf.Lerp(bottomClose, bottomOpen, elapsedTime / fadeDuration), 0);
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            top.position = new (0, Mathf.Lerp(topOpen, topClose, elapsedTime / fadeDuration), 0);
            bottom.position= new(0, Mathf.Lerp(bottomOpen,bottomClose,  elapsedTime / fadeDuration), 0);
            yield return null;
        }
    }

}
