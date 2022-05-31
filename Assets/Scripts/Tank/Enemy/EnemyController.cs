using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : TankBase
{
    protected WaitForSeconds delay;
    protected Coroutine moveCoroutine;

    private bool moveNext = true;

    
    protected override void Update()
    {
        animator.SetFloat("Health", health);
        animator.SetInteger("Tier", (int)tier);
        if (moveNext)
        {
            RandomMove();
        }
    
    }

    void RandomMove()
    {
        float result = Random.Range(0, 100.0f);
        moveNext = false;
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        switch (result)
        {
            case >= 0 and < 50.0f:
                moveCoroutine = StartCoroutine(MoveDownCorotine());
                //move down
                break;
            case >= 50 and < 70.0f:
                moveCoroutine = StartCoroutine(MoveLeftCorotine());
                //move left
                break;
            case >= 70 and < 90.0f:
                moveCoroutine = StartCoroutine(MoveRightCorotine());
                //move right
                break;
            case >= 90:
                moveCoroutine = StartCoroutine(MoveUpCorotine());
                //move up
                break;
            default:
                break;
        }
    }


    protected void StuckDetection()
    {
        if(collsionDetection != null && collsionDetection.collided == true)
            {
                StopCoroutine(moveCoroutine);
                moveNext = true;
            }
    }
    protected IEnumerator MoveDownCorotine()
    {

        float elapeseTime = 0;
        float duration = Random.Range(1.0f, 5.5f);
        while (elapeseTime < duration)
        {
            while (pasueTank)
            {
                yield return null;
            }
            elapeseTime += Time.deltaTime;
            MoveDown();
            yield return null;
            StuckDetection();
        }

        MoveStop();
        moveNext = true;
    }
    protected IEnumerator MoveUpCorotine()
    {
        float elapeseTime = 0;
        float duration = Random.Range(3.0f, 10f);
        while (elapeseTime < duration)
        {
            while (pasueTank)
            {
                yield return null;
            }
            elapeseTime += Time.deltaTime;
            MoveUp();
            yield return null;
            StuckDetection();
        }

        MoveStop();
        moveNext = true;
    }
    protected IEnumerator MoveLeftCorotine()
    {
        float elapeseTime = 0;
        float duration = Random.Range(3.0f, 10f);
        while (elapeseTime < duration)
        {
            while (pasueTank)
            {
                yield return null;
            }
            elapeseTime += Time.deltaTime;
            MoveLeft();
            yield return null;
            StuckDetection();
        }

        MoveStop();
        moveNext = true;
    }

    protected IEnumerator MoveRightCorotine()
    {
        float elapeseTime = 0;
        float duration = Random.Range(3.0f, 10f);
        while (elapeseTime < duration)
        {
            while (pasueTank)
            {
                yield return null;
            }
            elapeseTime += Time.deltaTime;
            MoveRight();
            yield return null;
            StuckDetection();
        }

        MoveStop();
        moveNext = true;
    }

    protected virtual IEnumerator Fire()
    {
        while (true)
        {
            while (pasueTank)
            {
                yield return null;
            }
            Attack();
            yield return delay;
        }

    }

    protected override void DestroySelf()
    {
        if(explosionEffect != null)
        {
            Instantiate(explosionEffect,transform.position,Quaternion.identity);
        }
        gameManager.DecreaseEnemyCount();
        Destroy(gameObject);
        if (explosionSound != null)
        {
            mainAudioSource.PlayOneShot(explosionSound);
        }
    }
}
