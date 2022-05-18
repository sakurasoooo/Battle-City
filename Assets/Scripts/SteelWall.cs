using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public class SteelWall : WallBase
{
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
