using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketsPool : MonoBehaviour, IPauseInteractor
{
    [SerializeField] private BucketsRow topBuckets;
    [SerializeField] private BucketsRow rightBuckets;
    [SerializeField] private BucketsRow leftBuckets;
    [SerializeField] private BucketsRow bottomBuckets;

    [SerializeField] private BucketChedule bucketsChedule;

    private void Start()
    {
        
    }
    
    private void Update()
    {

    }

    public void SetArraysLengths(int gridWidth, int gridHeigth)
    {
        topBuckets.InitializeRow(gridWidth, bucketsChedule);
        bottomBuckets.InitializeRow(gridWidth, bucketsChedule);

        leftBuckets.InitializeRow(gridHeigth, bucketsChedule);
        rightBuckets.InitializeRow(gridHeigth, bucketsChedule);
    }

    public void SetBucketToRow(Bucket bucket, BucketSide side)
    {
        switch (side)
        {
            case BucketSide.top:
                topBuckets.AddBucket(bucket);

                break;
            case BucketSide.right:
                rightBuckets.AddBucket(bucket);

                break;
            case BucketSide.bottom:
                bottomBuckets.AddBucket(bucket);

                break;
            case BucketSide.left:
                leftBuckets.AddBucket(bucket);

                break;
            default:
                break;
        }
    }

    public void SetPause()
    {
        topBuckets.SetPause();
        rightBuckets.SetPause();
        leftBuckets.SetPause();
        bottomBuckets.SetPause();
    }

    public void SetUnpause()
    {
        topBuckets.SetUnpause();
        rightBuckets.SetUnpause();
        leftBuckets.SetUnpause();
        bottomBuckets.SetUnpause();
    }
}

[System.Serializable]
public class BucketChedule
{
    [SerializeField] private float startDelay;
    [Space]
    [SerializeField] private float minShowDuration;
    [SerializeField] private float maxShowDuration;
    [Space]
    [SerializeField] private float minBreakTime;
    [SerializeField] private float maxBreakTime;

    public float GetStartDelay()
    {
        return startDelay + GetBreakTime();
    }

    public float GetShowTime()
    {
        if (minShowDuration > maxShowDuration)
        {
            Debug.LogWarning("min show time shoud be less then max");
            return minShowDuration;
        }
        return Random.Range(minShowDuration, maxShowDuration);
    }

    public float GetBreakTime()
    {
        if (minBreakTime > maxBreakTime)
        {
            Debug.LogWarning("min break time shoud be less then max");
            return minBreakTime;
        }
        return Random.Range(minBreakTime, maxBreakTime);
    }
}