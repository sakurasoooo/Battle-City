using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public GameObject enemyIcon;
    private EnemyManager enemyManager;
    private int enemyCount;
    private void Awake()
    {
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
    }
    private void Start()
    {
        enemyCount = enemyManager.enemySum;
        for (int i = 0; i < enemyCount; i++)
        {
            Instantiate(enemyIcon, transform);
        }
    }

    private void Update()
    {
        enemyCount = enemyManager.enemySum;
        DecreaseOne();
    }

    public void DecreaseOne()
    {
        if (transform.childCount > enemyCount)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
