using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : TankBase
{
    protected WaitForSeconds delay ;

    

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
