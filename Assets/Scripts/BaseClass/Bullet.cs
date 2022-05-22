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

    // protected float moveSpeed { get; set; }
    protected float explosionWidth { get; set; }
    protected float explosionDepth { get; set; }

    protected bool isAlive;

    protected virtual void Awake()
    {
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        speed = 2.5f;
        damage = 100.0f;
        isAlive = true;
        explosionWidth = 0.50f;
        explosionDepth = 0.02f;
        // moveSpeed = speed;
    }

    protected virtual void Start()
    {
        if (fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }

        LevelUp();
    }

    protected virtual void LevelUp()
    {
        switch (tier)
        {
            case Tier.Tier1:
                break; //placeholder
            case Tier.Tier2:
                speed = speed * 1.5f;
                break;//
            case Tier.Tier3:
                speed = speed * 1.5f;
                break; // placeholder
            case Tier.Tier4:
                explosionDepth += 0.32f;
                speed = speed * 1.5f;
                break; // placeholder
            default:
                Debug.Log("No Tier Found!");
                break;
        }
    }
    protected virtual void Update()
    {
        Move();
        DebugDrawBox(headPos.position, new Vector2(explosionWidth, explosionDepth), Quaternion.Angle(transform.rotation, Quaternion.identity), Color.cyan, 0.0f); //DEBUG
    }

    protected virtual void Move()
    {


        transform.Translate(Forward() * speed * Time.deltaTime);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (isAlive)
        {

            if (other.gameObject.layer == LayerMask.NameToLayer("Border"))
            {
                DestroySelf();
            }

            else if (other.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                WallDetection();
                DestroySelf();
            }

            else if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
            {
                BulletDetection(other);
                DestroySelf();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                PlayerDetection(other);
                // DestroySelf();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                EnemyDetection(other);
                // DestroySelf();
            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("Base"))
            {
                other.SendMessage("DestroySelf", tier);
                DestroySelf();
            }

        }
    }

    //https://gamedev.stackexchange.com/questions/156543/how-does-physics2d-overlapboxall-work-in-unity-3d
    void DebugDrawBox(Vector2 point, Vector2 size, float angle, Color color, float duration)
    {

        var orientation = Quaternion.Euler(0, 0, angle);

        // Basis vectors, half the size in each direction from the center.
        Vector2 right = orientation * Vector2.right * size.x / 2f;
        Vector2 up = orientation * Vector2.up * size.y / 2f;

        // Four box corners.
        var topLeft = point + up - right;
        var topRight = point + up + right;
        var bottomRight = point - up + right;
        var bottomLeft = point - up - right;

        // Now we've reduced the problem to drawing lines.
        Debug.DrawLine(topLeft, topRight, color, duration);
        Debug.DrawLine(topRight, bottomRight, color, duration);
        Debug.DrawLine(bottomRight, bottomLeft, color, duration);
        Debug.DrawLine(bottomLeft, topLeft, color, duration);
    }

    protected virtual void BulletDetection(Collider2D other)
    {

    }

    protected virtual void PlayerDetection(Collider2D other)
    {

    }

    protected virtual void EnemyDetection(Collider2D other)
    {

    }

    private void WallDetection()
    {

        Collider2D[] result = Physics2D.OverlapBoxAll(headPos.position, new Vector2(explosionWidth, explosionDepth), Quaternion.Angle(transform.rotation, Quaternion.identity), LayerMask.GetMask("Wall"));
        foreach (Collider2D other in result)
        {
            other.SendMessage("DestroySelf", tier);
        }
    }

    private Vector2 Forward() => Vector2.up;

    public virtual void DestroySelf()
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
