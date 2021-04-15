using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketsRow : MonoBehaviour
{
    private Bucket[] buckets;
    private BucketChedule chedule;

    private bool inPauseMode;
    private bool isShowing;

    private float timer; // goes down
    private Bucket currentBucket;

    void Start()
    {
        isShowing = false;
        inPauseMode = false;
        timer = chedule.GetStartDelay();
    }

    void Update()
    {
        if (inPauseMode == false)
            ManageBuckets();
    }

    private void ManageBuckets()
    {
        if (timer <= 0f)
        {
            if (isShowing)
            {
                TryHideBucket();
            }
            else
            {
                ShowNewBucket();
            }
        }

        timer -= Time.deltaTime;
    }

    private void TryHideBucket()
    {
        if (currentBucket.squaresTaking == 0)
        {
            currentBucket.ReturnToPool();

            isShowing = false;
            timer = chedule.GetBreakTime();
        }
    }

    private void ShowNewBucket()
    {
        currentBucket = GetRandomBucketFromPool();
        currentBucket.GetFromPool();

        isShowing = true;
        timer = chedule.GetShowTime();
    }

    private Bucket GetRandomBucketFromPool()
    {
        Bucket randomBucket = buckets[Random.Range(0, buckets.Length)];
        while (randomBucket.canBeChosenForActivation == false)
        {
            randomBucket = buckets[Random.Range(0, buckets.Length)];
        }
        return randomBucket;
    }

    public void InitializeRow(int rowLength, BucketChedule chedule)
    {
        buckets = new Bucket[rowLength];
        this.chedule = chedule;
    }

    public void AddBucket(Bucket bucketToAdd)
    {
        for (int i = 0; i < buckets.Length; i++)
        {
            if (buckets[i] == null)
            {
                buckets[i] = bucketToAdd;
                return;
            }
        }
    }

    public void SetPause()
    {
        inPauseMode = true;
        foreach (var bucket in buckets)
        {
            bucket.SetPause();
        }
    }

    public void SetUnpause()
    {
        inPauseMode = false;
        foreach (var bucket in buckets)
        {
            bucket.SetUnpause();
        }
    }
}
