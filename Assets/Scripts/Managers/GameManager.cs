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

    private WaitForSeconds  detectionInterval;
    private WaitForSeconds  eneymyDetectionInterval;
    private WaitForSeconds  birthInterval;

    private int enemylives;
    private int playerlives;
    private GameObject[] enemyExist ;
    private EnemyManager enemyManager;

    private Coroutine playerConrotine;
    private Coroutine enemyConrotine;
    private void Awake()
    {
        detectionInterval = new WaitForSeconds(2.0f);
        eneymyDetectionInterval = new WaitForSeconds(5.0f);
        birthInterval =new WaitForSeconds(2.0f);

        //Start game
        playerlives = 3;
        
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
        enemyExist= new GameObject[4];
    }
    private void Start() {
        playerConrotine =  StartCoroutine(PlayerGenerator());
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

   private IEnumerator EnemyGenerator(){
       while(enemyManager.enemySum > 0)
       {
           for (int i = 0; i< 4;i++)
           {
            if(enemyExist[i] == null) //    if (enemyExist < 4)
           {
               int randPos = Random.Range(0,enemyBirthPos.Length);
               GameObject tank = enemyManager.GetNext();

               Instantiate(tankBirthWrapper, enemyBirthPos[randPos].position, Quaternion.identity);
               yield return birthInterval;
               enemyExist[i] = Instantiate(tank,enemyBirthPos[randPos].position,tank.transform.rotation);
            break;//    enemyExist++;
           }
           }
               yield return eneymyDetectionInterval ;

       }

   }
}
