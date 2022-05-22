using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
public abstract class WallBase : MonoBehaviour
{
    protected virtual void DestroySelf(Tier tier)
    {
        Destroy(gameObject);
    }
}
