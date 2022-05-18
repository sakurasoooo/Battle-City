using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(AudioSource))]
public abstract class TankBase : MonoBehaviour
{
    private BulletSpawnManager bulletSpawnManager;

    [Header("Audio")]
    public AudioClip idle;
    public AudioClip move;

    public AudioClip explosionSound;

    public int health { get; protected set; } // health points
    protected Tier tier { get; set; } // level 
    public bool hasShield { get; protected set; } // if health can reduce
    public bool hasEffect { get; protected set; } // generate reward if hit
    protected float moveSpeed { get; set; } // move speed
    protected float acceleration { get; set; } // move acceleration

    protected bool isAlive;
    protected Rigidbody2D rb2d;
    protected Animator animator;

    protected AudioSource audioSource;
    protected AudioSource mainAudioSource;

    protected virtual void Awake()
    {
        mainAudioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        bulletSpawnManager = GetComponentInChildren<BulletSpawnManager>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        isAlive = true;
        tier = Tier.Tier1;
        acceleration = 100.0f;
        moveSpeed = 1.0f;
        audioSource.loop = true;
        audioSource.clip = idle;
    }

    protected virtual void Update()
    {
        animator.SetFloat("Health", health);
        animator.SetInteger("Tier", (int)tier);
    }

    protected virtual void MoveUp()
    {
        audioSource.clip = move; // audio
        animator.SetBool("Move", true); // aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 0); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(0, Mathf.Clamp(Mathf.Abs(rb2d.velocity.y), 0, moveSpeed)); // limit velocity
    }

    protected virtual void MoveDown()
    {
        audioSource.clip = move; // audio
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 180); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(0, -Mathf.Clamp(Mathf.Abs(rb2d.velocity.y), 0, moveSpeed)); // limit velocity
    }

    protected virtual void MoveLeft()
    {
        audioSource.clip = move; // audio
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 90); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(-Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), 0, moveSpeed), 0); // limit velocity
    }

    protected virtual void MoveRight()
    {
        audioSource.clip = move; // audio
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 270); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), 0, moveSpeed), 0); // limit velocity
    }

    protected virtual void MoveStop()
    {
        audioSource.clip = idle; // audio
        animator.SetBool("Move", false);// aniamtion
        rb2d.velocity = Vector2.zero; // velocity
    }

    protected virtual void Attack()
    {
        bulletSpawnManager.Fire(tier, transform.rotation);
    }

    protected virtual Vector2 Forward() => transform.up;

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tank"))
        {
            rb2d.velocity = Vector2.zero; // velocity
        }
    }

    protected virtual void LevelUp()
    {
        switch (tier)
        {
            case Tier.Tier1:
                tier = Tier.Tier2;
                break;
            case Tier.Tier2:
                tier = Tier.Tier3;
                break;
            case Tier.Tier3:
                tier = Tier.Tier4;
                break;
            default:
                tier = Tier.Tier1;
                break;
        }
    }

    protected virtual void DestroySelf()
    {   
        Destroy(gameObject);
        if(explosionSound != null)
        {
            mainAudioSource.PlayOneShot(explosionSound);
        }
    }

}
