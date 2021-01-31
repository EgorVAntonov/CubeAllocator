using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketsPool : MonoBehaviour
{
    public Bucket[] topBuckets;
    public Bucket[] rightBuckets;
    public Bucket[] leftBuckets;
    public Bucket[] bottomBuckets;

    private bool inPauseMode;

    [SerializeField] private float startDelay;
    [Space]
    [SerializeField] private float minShowDuration;
    [SerializeField] private float maxShowDuration;
    [Space]
    [SerializeField] private float minBreakTime;
    [SerializeField] private float maxBreakTime;

    private void Start()
    {
        StartCoroutine(ManageTopBuckets());
        StartCoroutine(ManageRightBuckets());
        StartCoroutine(ManageBottomBuckets());
        StartCoroutine(ManageLeftBuckets());
    }
    /*
    private void Update()
    {
        ManageTopBuckets();
    }
    */
    IEnumerator ManageTopBuckets()
    {
        yield return new WaitForSeconds(startDelay + GetBreakTime());

        while (true)
        {
            Bucket currentBucket = GetRandomBucketFromPool(topBuckets);

            currentBucket.GetFromPool();
            yield return new WaitForSeconds(GetShowDuration());

            yield return new WaitUntil(() => currentBucket.squaresTaking == 0);
            currentBucket.ReturnToPool();
            yield return new WaitForSeconds(GetBreakTime());
        }
    }

    IEnumerator ManageRightBuckets()
    {
        yield return new WaitForSeconds(startDelay + GetBreakTime());

        while (true)
        {
            Bucket currentBucket = GetRandomBucketFromPool(rightBuckets);

            currentBucket.GetFromPool();
            yield return new WaitForSeconds(GetShowDuration());

            yield return new WaitUntil(() => currentBucket.squaresTaking == 0);
            currentBucket.ReturnToPool();
            yield return new WaitForSeconds(GetBreakTime());
        }
    }

    IEnumerator ManageBottomBuckets()
    {
        yield return new WaitForSeconds(startDelay + GetBreakTime());

        while (true)
        {
            Bucket currentBucket = GetRandomBucketFromPool(bottomBuckets);

            currentBucket.GetFromPool();
            yield return new WaitForSeconds(GetShowDuration());

            yield return new WaitUntil(() => currentBucket.squaresTaking == 0);
            currentBucket.ReturnToPool();
            yield return new WaitForSeconds(GetBreakTime());
        }
    }

    IEnumerator ManageLeftBuckets()
    {
        yield return new WaitForSeconds(startDelay + GetBreakTime());

        while (true)
        {
            Bucket currentBucket = GetRandomBucketFromPool(leftBuckets);

            currentBucket.GetFromPool();
            yield return new WaitForSeconds(GetShowDuration());

            yield return new WaitUntil(() => currentBucket.squaresTaking == 0);
            currentBucket.ReturnToPool();
            yield return new WaitForSeconds(GetBreakTime());
        }
    }

    private Bucket GetRandomBucketFromPool(Bucket[] pool)
    {
        Bucket randomBucket = pool[Random.Range(0, pool.Length)];
        while (randomBucket.canBeChosenForActivation == false)
        {
            randomBucket = pool[Random.Range(0, pool.Length)];
        }
        return randomBucket;
    }

    private float GetShowDuration()
    {
        if (minShowDuration > maxShowDuration)
        {
            Debug.LogWarning("min show time shoud be less then max");
            return minShowDuration;
        }
        return Random.Range(minShowDuration, maxShowDuration);
    }

    private float GetBreakTime()
    {
        if (minBreakTime > maxBreakTime)
        {
            Debug.LogWarning("min break time shoud be less then max");
            return minBreakTime;
        }
        return Random.Range(minBreakTime, maxBreakTime);
    }

    public void SetArraysLengths(int gridWidth, int gridHeigth)
    {
        topBuckets = new Bucket[gridWidth];
        bottomBuckets = new Bucket[gridWidth];

        leftBuckets = new Bucket[gridHeigth];
        rightBuckets = new Bucket[gridHeigth];
    }
}