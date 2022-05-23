using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : Bullet
{
    protected override void EnemyDetection(Collider2D other)
    {
        other.SendMessage("ReduceHP");
        DestroySelf();
    }

    protected override void PlayerDetection(Collider2D other)
    {
        DestroySelf();
    }
}
