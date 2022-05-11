using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class PlayerController : Tank
{
    [Header("Input")]
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode fire = KeyCode.J;
    private KeyCode activatedKey { get; set; }
    private bool moveKeyPressed { get; set; }
    private void Start()
    {
        
        tier = Tier.Tier1;
        acceleration = 100.0f;
        moveSpeed = 1.0f;
        moveKeyPressed = false;
        activatedKey = KeyCode.None;
        
        audioSource.Play();
    }
    private void Update()
    {
        if(!audioSource.isPlaying) {
            audioSource.Play();
        }
        Move();
        Fire();
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
}
