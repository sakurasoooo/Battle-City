using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;

public abstract class Bullet : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip fireSound;
    public AudioClip explosionSound;
    [Header("Effect")]
    public GameObject effect;
    [Header("Head Position")]
    public Transform headPos;

    protected float speed { get; set; }
    protected float damage { get; set; }
    protected AudioSource audioSource;
    public Tier tier { get; set; }
    public BulletSpawnManager bulletSpawnManager { get; set; }

    protected bool isAlive;

    protected virtual void Awake()
    {
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        speed = 2.0f;
        damage = 100.0f;
        isAlive = true;
    }

    protected virtual void Start()
    {
        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
    protected virtual void Update()
    {
        Move();
    }

    protected virtual void Move()
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

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
        {
            DestorySelf();
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            DestorySelf();
        }

        else if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            DestorySelf();
        }
    }

    protected virtual Vector2 Forward() => Vector2.up;

    public virtual void DestorySelf()
    {
        if (isAlive)
        {
            if (explosionSound != null)// audio
            {
                audioSource.PlayOneShot(explosionSound);
            }
            if (effect != null)//particle
            {
                Instantiate(effect, headPos.position, headPos.rotation);
            }
            bulletSpawnManager.bullets--;
            isAlive = false;
            Destroy(gameObject);
        }
    }
}
