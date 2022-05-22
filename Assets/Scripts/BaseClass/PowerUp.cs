using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class PowerUp : MonoBehaviour
{
    [Header("Audio")]
    public AudioClip BirthSound;
    public AudioClip UseSound;
    protected AudioSource audioSource;
    protected Animator animator;

    protected GameObject target;
    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GameObject.Find("Audio Manager").GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        if (BirthSound != null)
        {
            audioSource.PlayOneShot(BirthSound);

        }
    }
    protected virtual void Effect()
    {

    }

    public virtual void Use()
    {
        if (UseSound != null)
        {
            audioSource.PlayOneShot(UseSound);

        }
        Effect();
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        target = other.gameObject;
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Use();
                DestroySelf();
            }
    }

    public virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
}
