using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Tank tank;
    private void Awake() {
        // Instantiate(tank,transform);
        Instantiate<Tank>(tank,transform);
    }

 
}
