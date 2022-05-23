using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAttribute;
[RequireComponent(typeof(Animator))]
public class HeartWall : WallBase
{
    private Animator animator;
    public AudioClip explosionSound;
    private AudioSource audioSource;

    private GameManager gameManager;
    private void Awake() {
        animator = GetComponent<Animator>();
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    protected override void DestroySelf(Tier tier)
    {
        gameManager.SetGameOver();
        animator.SetTrigger("broken");
        if(explosionSound != null) 
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }
}
