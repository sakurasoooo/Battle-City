using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : PowerUp
{
    protected override void Effect()
    {
        Collider2D[] result = Physics2D.OverlapBoxAll(Vector2.zero,new Vector2 (13,13),0,LayerMask.GetMask("Enemy"));
        foreach (Collider2D other in result)
        {
            other.SendMessage("DestroySelf");
        }
    }
}
