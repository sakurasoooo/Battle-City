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
    private void Awake() {
        animator = GetComponent<Animator>();
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
    }
    protected override void DestroySelf(Tier tier)
    {
        animator.SetTrigger("broken");
        if(explosionSound != null) 
        {
            audioSource.PlayOneShot(explosionSound);
        }
    }
}
