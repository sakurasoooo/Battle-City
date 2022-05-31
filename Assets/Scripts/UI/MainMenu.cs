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
    private Option currentOption;

    private void Awake()
    {
        currentOption = Option.Player_1;
    }

    private void Update()
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

    private void NextScene()
    {
        switch (currentOption)
        {
            case Option.Player_1:
                SceneManager.LoadScene(1);
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
                cursor.anchoredPosition = new Vector2(-2.5f, -0.7f);
                break;
            case Option.Construction:
                cursor.anchoredPosition = new Vector2(-2.5f, -1.67f);
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
