using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class BulletSpawnManager : MonoBehaviour
{
    public GameObject bulletPrefab;
    public int bullets { get; set; }

    private void Awake()
    {
        bullets = 0;
    }

    public void Fire(Tier tier, Tier bulletTier, Quaternion rotation, Collider2D self, Collider2D other)
    {
        switch (tier)
        {
            case Tier.Tier1:
                if (bullets < 1)
                {
                    CreateBullet(bulletTier, rotation, self, other);
                }
                break;
            case Tier.Tier2:
                if (bullets < 1)
                {
                    CreateBullet(bulletTier, rotation, self, other);
                }
                break;
            case Tier.Tier3:
                if (bullets < 2)
                {
                    CreateBullet(bulletTier, rotation, self, other);
                }
                break;
            case Tier.Tier4:
                if (bullets < 2)
                {
                    CreateBullet(bulletTier, rotation, self, other);
                }
                break;
            default:
                Debug.Log("Tier not Found!");
                break;
        }
    }

    void CreateBullet(Tier tier, Quaternion rotation,Collider2D self, Collider2D other)
    {
        Bullet clone = Instantiate(bulletPrefab, transform.position, rotation).GetComponent<Bullet>();
        clone.tier = tier;
        clone.bulletSpawnManager = this;
        if(self != null) {
            clone.Ignore(self);
        }
        if(other != null) {
            clone.Ignore(other);
        }
        bullets++;
    }
}
