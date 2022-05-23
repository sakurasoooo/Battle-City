using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class BulletEnemy : Bullet
{
    protected override void PlayerDetection(Collider2D other)
    {
        other.SendMessage("ReduceHP");
        DestroySelf();
    }

    protected override void EnemyDetection(Collider2D other)
    {
        DestroySelf();
    }

    protected override void LevelUp()
    {
        switch (tier)
        {
            case Tier.Tier1:
                speed = speed * 0.75f;
                break; //placeholder
            case Tier.Tier2:
                speed = speed * 1.0f;
                break;//
            case Tier.Tier3:
                explosionDepth += 0.32f;
                speed = speed * 1.5f;
                break; // placeholder
            case Tier.Tier5:
                explosionDepth += 0.32f;
                speed = speed * 1.0f;
                break; // placeholder
            default:
                Debug.LogError("No Tier Found!");
                break;
        }
    }
}
