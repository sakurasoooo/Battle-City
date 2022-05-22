using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [Header("Sound")]
    public AudioClip battleStart;
    

    private AudioSource audioSource;
    private void Awake() {
        audioSource = GetComponent<AudioSource>();
        
    }
    private void Start() {
        audioSource.PlayOneShot(battleStart);
    }

   
}
