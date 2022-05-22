using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class BasicTank : EnemyController
{
    private void Start() {
        delay = new WaitForSeconds(2.0f);
        tier = Tier.Tier1;
        bulletTier = Tier.Tier1;
        health = 100;
        moveSpeed *= 0.8f;
        StartCoroutine(Fire());
        RandomEffect();
    }
}
