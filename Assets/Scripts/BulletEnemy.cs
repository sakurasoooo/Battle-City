using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : Bullet
{
    protected override void PlayerDetection(Collider2D other)
    {
        other.SendMessage("DestroySelf");
        DestroySelf();
    }
}
