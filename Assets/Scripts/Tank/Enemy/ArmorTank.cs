using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class ArmorTank : EnemyController
{
    private void Start() {
        delay = new WaitForSeconds(0.5f);
        tier = Tier.Tier1;
        bulletTier = Tier.Tier3;
        health = 400;
        acceleration *= 1.0f;
        moveSpeed *= 1.0f;
        StartCoroutine(Fire());
        RandomEffect();
    }
}

