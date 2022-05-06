using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Tank : MonoBehaviour
{
    private BulletSpawnManager bulletSpawnManager;
    
    public int health { get; protected set; }
    protected Tier tier { get; set; }
    public bool hasShield { get; protected set; }
    public bool hasEffect { get; protected set; }
    protected float moveSpeed { get; set; }
    protected float acceleration { get; set; }
    protected Rigidbody2D rb2d;
    protected Animator animator;

   protected void Awake() {
        bulletSpawnManager = GetComponentInChildren<BulletSpawnManager>();
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    protected virtual void MoveUp()
    {
        animator.SetBool("Move", true); // aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 0); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(0, Mathf.Clamp(Mathf.Abs(rb2d.velocity.y), 0, moveSpeed)); // limit velocity
    }

    protected virtual void MoveDown()
    {
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 180); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(0, -Mathf.Clamp(Mathf.Abs(rb2d.velocity.y), 0, moveSpeed)); // limit velocity
    }

    protected virtual void MoveLeft()
    {
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 90); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(-Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), 0, moveSpeed), 0); // limit velocity
    }

    protected virtual void MoveRight()
    {
        animator.SetBool("Move", true);// aniamtion
        transform.rotation = Quaternion.Euler(0, 0, 270); // Rotation
        rb2d.AddForce(Forward() * acceleration * Time.deltaTime); // velocity
        rb2d.velocity = new Vector2(Mathf.Clamp(Mathf.Abs(rb2d.velocity.x), 0, moveSpeed), 0); // limit velocity
    }

    protected virtual void MoveStop()
    {
        animator.SetBool("Move", false);// aniamtion
        rb2d.velocity = Vector2.zero; // velocity
    }

    protected virtual void Attack()
    {
        bulletSpawnManager.Fire(tier,transform.rotation);
    }

    protected Vector2 Forward() => transform.up;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Tank"))
        {
            rb2d.velocity = Vector2.zero; // velocity
        }
    }

}
