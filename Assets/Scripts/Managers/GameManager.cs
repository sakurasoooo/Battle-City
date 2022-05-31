using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform playerABirthPos;
    public TankBirthWrapper tankBirthWrapper;
    public Transform[] enemyBirthPos;
    public static GameObject playerA = null;

    private WaitForSeconds detectionInterval;
    private WaitForSeconds eneymyDetectionInterval;
    private WaitForSeconds birthInterval;

    private int enemylives;
    private int playerlives;
    private GameObject[] enemyExist;
    private EnemyManager enemyManager;
    private UIManager uIManager;

    private Coroutine playerConrotine;
    private Coroutine enemyConrotine;

    public bool gameOver { get; private set; }
    public bool gameWin { get; private set; }
    private int enemyCount { get; set; }
    private void Awake()
    {
        detectionInterval = new WaitForSeconds(2.0f);
        eneymyDetectionInterval = new WaitForSeconds(5.0f);
        birthInterval = new WaitForSeconds(2.0f);

        //Start game
        playerlives = 3;
        enemyCount = 0;
        gameOver = false;
        gameWin = false;
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        uIManager = GameObject.FindObjectOfType<UIManager>();
        enemyExist = new GameObject[4];
    }

    private void Update()
    {
        if (!gameOver &&!gameWin)
        {
            if (playerlives <= 0 && playerA == null)
            {
                SetGameOver();
            }

            if(enemyCount <= 0 && enemyManager.enemySum <=0)
            {
                SetGameWin();
            }
        }

    }

    // private int EnemyCount()
    // {
    //     int count = 0;
    //     foreach(GameObject g in enemyExist)
    //     {
    //         if (g != null)
    //         count++;
    //     }
    //     return count;
    // }

    public void SetGameOver()
    {
        Debug.Log("Lose");
        gameOver = true;
        uIManager.GameOver();
    }

    public void SetGameWin()
    {
        Debug.Log("Win");
        gameWin = true;
    }
    private void Start()
    {
        playerConrotine = StartCoroutine(PlayerGenerator());
        enemyConrotine = StartCoroutine(EnemyGenerator());
    }
    private IEnumerator PlayerGenerator()
    {
        if (playerA == null)
        {
            Instantiate(tankBirthWrapper, playerABirthPos.position, Quaternion.identity);
            yield return birthInterval;
            playerA = Instantiate(PlayerPrefab, playerABirthPos.position, Quaternion.identity);
            playerlives--;
        }
        while (playerlives > 0)
        {
            if (playerA == null)
            {
                Instantiate(tankBirthWrapper, playerABirthPos.position, Quaternion.identity);
                yield return birthInterval;
                playerA = Instantiate(PlayerPrefab, playerABirthPos.position, Quaternion.identity);
                playerlives--;
            }

            yield return detectionInterval;
        }
    }

    private IEnumerator EnemyGenerator()
    {
        while (enemyManager.enemySum > 0)
        {
            for (int i = 0; i < 4; i++)
            {
                if (enemyExist[i] == null) //    if (enemyExist < 4)
                {
                    enemyCount++;
                    int randPos = Random.Range(0, enemyBirthPos.Length);
                    GameObject tank = enemyManager.GetNext();

                    Instantiate(tankBirthWrapper, enemyBirthPos[randPos].position, Quaternion.identity);
                    yield return birthInterval;
                    enemyExist[i] = Instantiate(tank, enemyBirthPos[randPos].position, tank.transform.rotation);
                    break;//    enemyExist++;
                }
            }
            yield return eneymyDetectionInterval;

        }

    }

    public void DecreaseEnemyCount() => enemyCount--;

    public void AddPlayerLives() => playerlives++;
    
}
