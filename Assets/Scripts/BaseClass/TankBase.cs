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

    [Header("Shield")]
    public GameObject shieldNormal;
    public GameObject shieldSmall;

    public int health { get; protected set; } // health points
    protected Tier tier { get; set; } // level 
    protected Tier bulletTier { get; set; } // level 
    public bool hasShield { get; protected set; } // if health can reduce
    public bool hasEffect { get; protected set; } // generate reward if hit
    protected float moveSpeed { get; set; } // move speed
    protected float acceleration { get; set; } // move acceleration

    protected bool onSnow;
    protected static bool pasueTank = false;
    protected Rigidbody2D rb2d;
    protected Animator animator;

    protected AudioSource audioSource;
    protected AudioSource mainAudioSource;
    protected BoxCollider2D boxCollider2D;

    private Coroutine slipCoroutine;
    protected PowerUpManager powerUpManager;
    protected GameManager gameManager;
    
    protected GameObject shield;

    protected CollsionDetection collsionDetection;

    protected virtual void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();

        mainAudioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        bulletSpawnManager = GetComponentInChildren<BulletSpawnManager>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        powerUpManager = GameObject.FindObjectOfType<PowerUpManager>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        collsionDetection = GetComponent<CollsionDetection>();
        onSnow = false;
        health = 100;
        tier = Tier.Tier1;
        bulletTier = Tier.Tier1;
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

    protected virtual void BirthProtection()
    {
        if (shieldSmall != null)
        {
            shield = Instantiate(shieldSmall, transform);
        }
    }

    public virtual void AddShield()
    {
        if (shieldNormal != null)
        {
            if (shield != null)
            {
                Destroy(shield);
            }
            shield = Instantiate(shieldNormal, transform);
        }
    }


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

        audioSource.clip = idle; // audio
        animator.SetBool("Move", false);// aniamtion

        if (onSnow&& gameManager.gameOver == false)
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
        for (int i = 0; i < 5; i++)
        {
            if (onSnow == false)
            {
                break;
            }
            yield return interval;
        }
        CannotMove();
    }

    private void StopSlip()
    {
        if (slipCoroutine != null)
        {
            StopCoroutine(slipCoroutine);
            slipCoroutine = null;
        }
    }

    protected virtual void Attack()
    {
        bulletSpawnManager.Fire(tier, bulletTier, transform.rotation, boxCollider2D, shield ? shield.GetComponent<BoxCollider2D>() : null);
    }

    protected virtual Vector2 Forward() => transform.up;



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

    public static void Unpause()
    {
        pasueTank = false;
    }

    public virtual void PauseTank()
    {
        pasueTank = true;
        MoveStop();

    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
        if (explosionSound != null)
        {
            mainAudioSource.PlayOneShot(explosionSound);
        }
    }
    protected virtual void RandomEffect()
    {
        int result = Random.Range(0, 100);
        switch (result)
        {
            case >= 0 and < 33:
                EnableEffect();
                break;
            default:
                break;
        }
    }
    protected virtual void EnableEffect()
    {
        hasEffect = true;
        animator.SetBool("Effect", true);
    }

    protected virtual void DisableEffect()
    {
        hasEffect = false;
        animator.SetBool("Effect", false);
    }

    protected virtual void ReduceHP()
    {
        if (hasEffect)
        {
            powerUpManager.GeneratePowerUp();
            // generate item
            DisableEffect();
        }
        health -= 100;
    }

}
