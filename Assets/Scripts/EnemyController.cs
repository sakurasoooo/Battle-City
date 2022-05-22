using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : TankBase
{
    protected WaitForSeconds delay = new WaitForSeconds(1.0f);

    private void Start() {
        health = 100;
        StartCoroutine(Fire());
    }

    protected override void Update()
    {
        animator.SetFloat("Health", health);
        animator.SetInteger("Tier", (int)tier);
        MoveDown();
    }
    protected virtual IEnumerator Fire()
    {
        while(isAlive)
        {
            Attack();
        yield return delay;
        }
    }
}
