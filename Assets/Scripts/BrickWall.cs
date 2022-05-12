using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            DestorySelf();
        }
    }

    public void DestorySelf() {
        Destroy(gameObject);
    }
}
