using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class UIManager : MonoBehaviour
{

    [Header("Pause Sound")]
    public AudioClip pauseSound;
    [Header("Text")]
    public GameObject gameOver;
    public GameObject pause;
    private GameManager gameManager;
    private AudioSource audioSource;
    // private AudioListener audioListener;
    public bool isPause { get; private set; }
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
        isPause = false;
        audioSource.ignoreListenerPause = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (isPause)
            {
                isPause = false;
                Unpause();
            }
            else
            {
                isPause = true;
                if (gameManager.gameOver != true)
                {
                    Pause();
                }
            }
        }
    }
    public void GameOver()
    {
        gameOver.SetActive(true);
    }

    public void Pause()
    {
        if (pauseSound != null)
        {
            audioSource.PlayOneShot(pauseSound);
        }
        pause.SetActive(true);
        Time.timeScale = 0;
        AudioListener.pause = true;
    }

    public void Unpause()
    {
        pause.SetActive(false);
        Time.timeScale = 1;
        AudioListener.pause = false;
    }
}
