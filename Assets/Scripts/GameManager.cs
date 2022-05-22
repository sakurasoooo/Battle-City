using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerPrefab;
    public Transform playerABirthPos;
    public static GameObject playerA = null;

    private void Awake()
    {
        if (playerA == null)
        {
            playerA = Instantiate(PlayerPrefab, playerABirthPos.position, Quaternion.identity);
        }
        StartCoroutine(PlayerGenerator());
    }
    private IEnumerator PlayerGenerator()
    {
        while (true)
        {
            if (playerA == null)
            {
                yield return new WaitForSeconds(2.0f);
                playerA = Instantiate(PlayerPrefab, playerABirthPos.position, Quaternion.identity);
            }

            yield return null;
        }
    }

    // private void OnDestroy() {
    //     if (playerA != null)
    //     {
    //         Destroy(playerA);
    //     }
    // }
}
