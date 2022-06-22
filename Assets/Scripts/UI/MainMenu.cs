using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    enum Option
    {
        Player_1,
        Construction
    }
    public RectTransform cursor;
    private RectTransform m_RectTransform;
    public Animator tankAnim;
    private Option currentOption;
    private TransitionManager transitionManager;
    private bool ready;

    private bool keyLock = false;

    private void Awake()
    {
        transitionManager = GameObject.FindObjectOfType<TransitionManager>();
        ready = false;
        m_RectTransform = GetComponent<RectTransform>();
        currentOption = Option.Player_1;
        tankAnim.SetBool("Move", true);
        Time.timeScale = 0;

    }


    private void Update()
    {
        if (ready == false)
        {
            m_RectTransform.anchoredPosition = new Vector2(0, Mathf.Lerp(-900, 0, Time.unscaledTime * 0.1f));

            if (Input.anyKeyDown || m_RectTransform.anchoredPosition.y >= 0.0f)
            {
                m_RectTransform.anchoredPosition = Vector2.zero;
                ready = true;
                Time.timeScale = 1;
            }
        }
        else
        {
            if (keyLock == false)
            {
                setCursor();
                if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
                {
                    GetNext();
                }
                else if (Input.GetKeyDown(KeyCode.J))
                {
                    NextScene();
                }
            }
        }
    }

    private void NextScene()
    {
        keyLock = true;
        switch (currentOption)
        {
            case Option.Player_1:
                // SceneManager.LoadScene(1);
                if (transitionManager != null)
                {
                    transitionManager.StartNextLevel(1);
                }
                break;
            case Option.Construction:
                Debug.Log("LOAD CONS");
                break;
            default:
                break;
        }
    }

    private void setCursor()
    {
        switch (currentOption)
        {
            case Option.Player_1:
                cursor.anchoredPosition = new Vector2(-264.6f, -75.6f);
                break;
            case Option.Construction:
                cursor.anchoredPosition = new Vector2(-264.6f, -180.67f);
                break;
            default:
                break;
        }
    }

    private void GetNext()
    {
        switch (currentOption)
        {
            case Option.Player_1:
                currentOption = Option.Construction;
                break;
            case Option.Construction:
                currentOption = Option.Player_1;
                break;
            default:
                break;
        }
    }
}
