using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBirthWrapper : MonoBehaviour
{
    // public GameObject tank;

    private void Start() {
        StartCoroutine(Delay());
    }
    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(2.5f);
        // Instantiate(tank,transform.position,tank.transform.rotation);
        Destroy(gameObject);
    }
}
