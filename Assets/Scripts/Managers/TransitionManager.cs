using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance;
    public Transform top;
    public Transform bottom;
    private float fadeDuration;
    private float topClose;
    private float topOpen;
    private float bottomClose;
    private float bottomOpen;

    private Coroutine fade;
    private void Awake()
    {
        fadeDuration = 1.2f;
        bottomClose = -3.8661f;
        topOpen = 9.6074f;
        topClose = 4.5674f;
        bottomOpen = -8.9861f;

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
    
    }

    public void StartNextLevel(int index)
    {
        StartCoroutine(NextLevel(index));
    }

    private IEnumerator NextLevel(int index)
    {
        yield return FadeIn();
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + index);
        yield return new WaitForSeconds(1.0f);
        yield return FadeOut();
    }

    public void StartFadeOut()
    {
        fade = StartCoroutine(FadeOut());
    }

    public void StartFadeIn()
    {
       fade = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            top.position = new(2.8433f, Mathf.Lerp(topClose, topOpen, elapsedTime / fadeDuration), 0);
            bottom.position = new(2.9715f, Mathf.Lerp(bottomClose, bottomOpen, elapsedTime / fadeDuration), 0);
            yield return null;
        }
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0.0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            top.position = new(2.8433f, Mathf.Lerp(topOpen, topClose, elapsedTime / fadeDuration), 0);
            bottom.position = new(2.9715f, Mathf.Lerp(bottomOpen, bottomClose, elapsedTime / fadeDuration), 0);
            yield return null;
        }
    }

}
