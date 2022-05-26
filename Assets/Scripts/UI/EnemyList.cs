using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyList : MonoBehaviour
{
    public GameObject enemyIcon;
    private EnemyManager enemyManager;
    private int enemyCount;
    private void Awake() {
        enemyManager = GameObject.FindObjectOfType<EnemyManager>();
    }
    private void Start() {
        enemyCount = enemyManager.enemySum;
        
    }
}
