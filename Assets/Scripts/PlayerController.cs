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
    }
    private void Update()
    {
        Move();
        Fire();
    }

    void Move()
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

        bool IsKeyPressed(KeyCode key) => Input.GetKey(key) && (activatedKey == KeyCode.None || activatedKey == key);

        bool IsKeyUp(KeyCode key) => Input.GetKeyUp(key) && activatedKey == key;
    }

    void Fire(){
        if(Input.GetKeyDown(fire)){
            Attack();
        }
    }
}
