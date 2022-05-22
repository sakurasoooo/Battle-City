using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
[RequireComponent(typeof(Animator))]
public class SteelWall : WallBase
{

    private Animator animator;
    private void Awake() {
        animator = GetComponent<Animator>();
        
    }

    public void Flash()
    {
        animator.SetTrigger("Flash");
    }
    protected override void DestroySelf(Tier tier)
    {
        switch (tier)
        {
            case Tier.Tier1:
                break;
            case Tier.Tier2:
                break;
            case Tier.Tier3:
                break;
            case Tier.Tier4:
                Destroy(gameObject);
                break;
            default:
                break;
        }
    }
}
