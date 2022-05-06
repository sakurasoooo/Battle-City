using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class Bullet : MonoBehaviour
{
    private float speed { get; set; }
    private float damage { get; set; }
    public Tier tier { get; set; }
    public BulletSpawnManager bulletSpawnManager { get; set; }

    private void Awake()
    {
        speed = 2.0f;
        damage = 100.0f;
    }
    private void Update()
    {
        Move();
    }

    void Move()
    {
        float moveSpeed = speed;
        switch (tier)
        {
            case Tier.Tier1:
                break; //placeholder
            case Tier.Tier2:
                moveSpeed = speed * 1.5f;
                break;//
            case Tier.Tier3:
                break; // placeholder
            case Tier.Tier4:
                break; // placeholder
            default:
                Debug.Log("No Tier Found!");
                break;
        }

        transform.Translate(Forward() * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            DestorySelf();
        }
    }

    Vector2 Forward() => Vector2.up;

    public void DestorySelf()
    {
        //particle
        bulletSpawnManager.bullets--;
        Destroy(gameObject);
    }
}
