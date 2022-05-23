using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : PowerUp
{
    protected override void Effect()
    {
        GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
        gameManager.AddPlayerLives();
    }
}