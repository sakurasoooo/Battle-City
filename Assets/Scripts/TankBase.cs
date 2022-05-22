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
    public AudioClip slip;

    public AudioClip explosionSound;

    public int health { get; protected set; } // health points
    protected Tier tier { get; set; } // level 
    public bool hasShield { get; protected set; } // if health can reduce
    public bool hasEffect { get; protected set; } // generate reward if hit
    protected float moveSpeed { get; set; } // move speed
    protected float acceleration { get; set; } // move acceleration

    // private bool canMove;
    protected bool isAlive;
    protected bool onSnow;
    protected Rigidbody2D rb2d;
    protected Animator animator;

    protected AudioSource audioSource;
    protected AudioSource mainAudioSource;

    private Coroutine slipCoroutine;

    protected virtual void Awake()
    {
        mainAudioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        bulletSpawnManager = GetComponentInChildren<BulletSpawnManager>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        onSnow = false;
        health = 100;
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

    // protected virtual void FixedUpdate() {
    //     if(!canMove)
    //     {
    //         rb2d.velocity = Vector2.zero;
    //     }
    // }

    protected virtual void LateUpdate()
    {
        if (health <= 0)
        {
            DestroySelf();
        }
    }

    protected virtual void CanMove()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected virtual void CannotMove()
    {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    protected virtual void MoveUp()
    {
        StopSlip();
        CanMove();
        audioSource.clip = move; // audio
        animator.SetBool("Move", true); // aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 0); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(0, Mathf.Clamp(Mathf.Abs(rb2d.velocity.y), 0, moveSpeed)); // limit velocity
    }

    protected virtual void MoveDown()
    {
        StopSlip();
        CanMove();
        audioSource.clip = move; // audio
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 180); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(0, -Mathf.Clamp(Mathf.Abs(rb2d.velocity.y), 0, moveSpeed)); // limit velocity
    }

    protected virtual void MoveLeft()
    {
        StopSlip();
        CanMove();
        audioSource.clip = move; // audio
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 90); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(-Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), 0, moveSpeed), 0); // limit velocity
    }

    protected virtual void MoveRight()
    {
        StopSlip();
        CanMove();
        audioSource.clip = move; // audio
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 270); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), 0, moveSpeed), 0); // limit velocity
    }

    protected virtual void MoveStop()
    {
        Debug.Log(rb2d.velocity);

        audioSource.clip = idle; // audio
        animator.SetBool("Move", false);// aniamtion

        if (onSnow)
        {
            if (slip != null)
            {
                mainAudioSource.PlayOneShot(slip);

            }
            if (rb2d.velocity.x > 0.01f || rb2d.velocity.x < -0.01f)
            {
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionY;
            }
            else if (rb2d.velocity.y > 0.01f || rb2d.velocity.y < -0.01f)
            {
                rb2d.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
            }
            slipCoroutine = StartCoroutine(SlipCoroutine());
        }
        else
        {
            rb2d.velocity = Vector2.zero; // velocity
            CannotMove();
        }

    }

    private IEnumerator SlipCoroutine()
    {
        WaitForSeconds interval = new WaitForSeconds(0.1f);
        for (int i = 0; i< 5;i++) 
        {   
            if(onSnow == false)
            {
                break;
            }
            yield return interval;
        }
        CannotMove();
    }

    private void StopSlip()
    {
        if(slipCoroutine != null)
        {
            StopCoroutine(slipCoroutine);
            slipCoroutine = null;
        }
    }

    protected virtual void Attack()
    {
        bulletSpawnManager.Fire(tier, transform.rotation);
    }

    protected virtual Vector2 Forward() => transform.up;

    // protected virtual void OnCollisionStay2D(Collision2D other)
    // {
    //     if (other.gameObject.layer == LayerMask.NameToLayer("Player") || other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
    //     {
    //         Vector2 direction =  other.transform.position - transform.position;
    //         direction = direction.normalized;
    //         Debug.Log(direction);

    //         Vector2 v = Vector3.Project(rb2d.velocity,direction);
    //         float flag = Vector3.Dot(v.normalized,direction);
    //         if(flag > 0.99f)
    //         {
    //             rb2d.velocity += (-v);
    //         }
    //         // if (flag < -0.99)
    //         // {
    //         //     rb2d.velocity += (v);
    //         // }
    //     }
    // }

    protected virtual void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Snow"))
        {
            onSnow = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Snow"))
        {
            onSnow = false;
        }
    }

    protected virtual void LevelUp()
    {
        switch (tier)
        {
            case Tier.Tier1:
                Tier2();
                tier = Tier.Tier2;
                break;
            case Tier.Tier2:
                Tier3();
                tier = Tier.Tier3;
                break;
            case Tier.Tier3:
                Tier4();
                tier = Tier.Tier4;
                break;
            case Tier.Tier4:
                // do nothing
                break;
            default:
                Tier1();
                tier = Tier.Tier1;
                break;
        }
    }

    protected virtual void Tier1()
    {

    }
    protected virtual void Tier2()
    {

    }
    protected virtual void Tier3()
    {

    }
    protected virtual void Tier4()
    {

    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
        if (explosionSound != null)
        {
            mainAudioSource.PlayOneShot(explosionSound);
        }
    }

    protected virtual void ReduceHP()
    {
        health -= 100;
    }

}
