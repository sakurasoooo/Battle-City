using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public List<GameObject> enemyList;
    public GameObject[] enemyTypes;
    public int[] enemyNumbers;
   public int enemySum {get; private set;}
    private void Awake()
    {
      
        for (int index = 0; index < enemyTypes.Length; index++)
        {
            for (int j = 0; j < enemyNumbers[index]; j++)
            {
                int randInddex = Random.Range(0, enemyList.Count);
                enemyList.Insert(randInddex, enemyTypes[index]);
                enemySum++;
            }
        }
    }

   public GameObject GetNext()
   {
      enemySum--;
      if(enemyList.Count > 0)
      {
         GameObject tank = enemyList[0];
         enemyList.RemoveAt(0);
         return tank;
      }
      return enemyTypes[0];
   }
}
