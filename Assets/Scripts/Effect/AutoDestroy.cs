using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public void SelfDestroy()
    {
        Destroy(gameObject);
    }

 
}
