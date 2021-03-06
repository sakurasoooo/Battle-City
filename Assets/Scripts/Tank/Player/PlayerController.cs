using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class PlayerController : TankBase
{
    [Header("Input")]
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode fire = KeyCode.J;
    private KeyCode activatedKey { get; set; }
    private bool moveKeyPressed { get; set; }
    private UIManager uIManager;
    private void Start()
    {
        moveSpeed *= 1.1f;
        moveKeyPressed = false;
        activatedKey = KeyCode.None;
        BirthProtection();
        audioSource.Play();
        uIManager = GameObject.FindObjectOfType<UIManager>();

        // DontDestroyOnLoad(gameObject);
    }

    // private void OnGUI()
    // {
    //     if (GUI.Button(new Rect(1, 10, 50, 25), "Tier 1"))
    //     {
    //         tier = Tier.Tier1;
    //     }
    //     if (GUI.Button(new Rect(1, 60, 50, 25), "Tier 2"))
    //     {
    //         Tier2();
    //         tier = Tier.Tier2;
    //     }
    //     if (GUI.Button(new Rect(1, 110, 50, 25), "Tier 3"))
    //     {
    //         tier = Tier.Tier3;
    //     }
    //     if (GUI.Button(new Rect(1, 160, 50, 25), "Tier 4"))
    //     {
    //         tier = Tier.Tier4;
    //     }
    //     if (GUI.Button(new Rect(1, 210, 70, 25), "Level Up"))
    //     {
    //         LevelUp();
    //     }

    //     if (GUI.Button(new Rect(1, 260, 70, 25), "Destroy"))
    //     {
    //         DestroySelf();
    //     }
    // }
    protected override void Update()
    {
        animator.SetFloat("Health", health);
        animator.SetInteger("Tier", (int)tier);
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
        if (gameManager.gameOver != true && uIManager.isPause != true)
        {
            Move();
            Fire();
        }
        // Trigger once
        if (gameManager.gameOver)
        {
            MoveStop();
        }
    }

    private void Move()
    {
        if (IsKeyPressed(up))
        {
            activatedKey = up;
            MoveUp();
        }

        if (IsKeyPressed(down))
        {
            activatedKey = down;
            MoveDown();
        }

        if (IsKeyPressed(left))
        {
            activatedKey = left;
            MoveLeft();
        }

        if (IsKeyPressed(right))
        {
            activatedKey = right;
            MoveRight();
        }

        if (
            IsKeyUp(up)
            || IsKeyUp(down)
            || IsKeyUp(left)
            || IsKeyUp(right)
            )
        {
            activatedKey = KeyCode.None;
            MoveStop();
        }
    }
    private bool IsKeyPressed(KeyCode key) => Input.GetKey(key) && (activatedKey == KeyCode.None || activatedKey == key);

    private bool IsKeyUp(KeyCode key) => Input.GetKeyUp(key) && activatedKey == key;
    private void Fire()
    {
        if (Input.GetKeyDown(fire))
        {
            Attack();
        }
    }

    protected override void Tier2()
    {
        acceleration *= 2.0f;
        moveSpeed *= 1.8f;
        bulletTier = Tier.Tier2;
    }
    protected override void Tier3()
    {
        bulletTier = Tier.Tier3;
    }
    protected override void Tier4()
    {
        bulletTier = Tier.Tier4;
    }
}
