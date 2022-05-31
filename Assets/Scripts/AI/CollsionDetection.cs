using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollsionDetection : MonoBehaviour
{
    public bool collided { get; private set; }
    private Rigidbody2D rb2D;
    private BoxCollider2D b2d;
    private void Awake()
    {
        collided = false;
        rb2D = GetComponent<Rigidbody2D>();
        b2d = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {

        if (rb2D != null && b2d != null)
        {
            Vector2 position = transform.position;
            Vector2 direction = transform.up;
            int maxResult = 10;
            RaycastHit2D[] results = new RaycastHit2D[maxResult];
            int num = b2d.Cast(direction, results, 0.05f);


            if (num > 0)
            {
                foreach (RaycastHit2D target in results)
                {
                    //Note: need more on NULL check
                    if (target.collider != null && target.collider.isTrigger == false)
                    {
                        collided = true;
                        break;
                    }

                    collided = false;

                }
            }
            else
            {
                collided = false;
            }
        }
    }
}
