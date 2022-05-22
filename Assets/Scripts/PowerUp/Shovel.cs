using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : PowerUp
{
    private BaseManager baseManager;
    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
        baseManager = GameObject.FindObjectOfType<BaseManager>();
    }
    protected override void Effect()
    {
        baseManager.Renforce();
    }
}
