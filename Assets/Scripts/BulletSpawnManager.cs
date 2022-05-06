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

    public void Fire(Tier tier, Quaternion rotation)
    {
        switch (tier)
        {
            case Tier.Tier1:
                if (bullets < 1)
                {
                    CreateBullet(tier, rotation);
                }
                break;
            case Tier.Tier2:
                if (bullets < 1)
                {
                    CreateBullet(tier, rotation);
                }
                break;
            case Tier.Tier3:
                if (bullets < 2)
                {
                    CreateBullet(tier, rotation);
                }
                break;
            case Tier.Tier4:
                if (bullets < 2)
                {
                    CreateBullet(tier, rotation);
                }
                break;
            default:
                Debug.Log("Tier not Found!");
                break;
        }
    }

    void CreateBullet(Tier tier, Quaternion rotation)
    {
        Bullet clone = Instantiate(bulletPrefab, transform.position, rotation).GetComponent<Bullet>();
        clone.tier = tier;
        clone.bulletSpawnManager = this;
        bullets++;
    }
}
