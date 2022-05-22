using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class FastTank : EnemyController
{
    private void Start() {
        delay = new WaitForSeconds(1.0f);
        tier = Tier.Tier1;
        bulletTier = Tier.Tier2;
        health = 100;
        acceleration *= 2.0f;
        moveSpeed *= 2.0f;
        StartCoroutine(Fire());
        RandomEffect();
    }
}
