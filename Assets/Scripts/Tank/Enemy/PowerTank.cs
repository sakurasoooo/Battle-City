using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class PowerTank : EnemyController
{
    private void Start() {
        delay = new WaitForSeconds(1.0f);
        tier = Tier.Tier1;
        bulletTier = Tier.Tier5;
        health = 100;
        acceleration *= 1.0f;
        moveSpeed *= 1.0f;
        StartCoroutine(Fire());
        RandomEffect();
    }
}
