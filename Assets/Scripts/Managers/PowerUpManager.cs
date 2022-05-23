using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    private GameObject powerUp;
    [Header("PowerUps")]
    public GameObject[] powerUplist;
    // Start is called before the first frame update
    
    public void GeneratePowerUp()
    {
        if (powerUp != null)
        {
            Destroy(powerUp);
        }
        int index = Random.Range(0,powerUplist.Length);
        powerUp = Instantiate(powerUplist[index],RandonPos(),Quaternion.identity);

    }

    private Vector2 RandonPos()
    {
        return new Vector2 (Random.Range(-3.5f,4.13f),Random.Range(4.15f,-3.5f));
    }
}
