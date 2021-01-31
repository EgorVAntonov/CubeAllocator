using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BucketInteractor : MonoBehaviour, IPauseInteractor
{
    private bool hasCollected = false;
    private bool inPauseMode = false;
    private Bucket collectedBucket;

    [Header("animation options")]
    [SerializeField] private float animationSpeed;
    [SerializeField] private float turningSpeed;
    [SerializeField] private float shrinkingModifier;

    void Start()
    {
        hasCollected = false;
    }

    private void Update()
    {
        HandleBucketAnimation();
    }

    private void HandleBucketAnimation()
    {
        if (hasCollected && inPauseMode == false)
        {
            GetComponent<Animator>().enabled = false;
            transform.position = Vector3.MoveTowards(transform.position, collectedBucket.transform.position, animationSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, 0f, turningSpeed * Time.deltaTime));
            transform.localScale *= shrinkingModifier;
            if (transform.position == collectedBucket.transform.position)
            {
                collectedBucket.squaresTaking--;
                Destroy(gameObject);
            }
        }
    }

    public void StartAnimationWithBucket(Bucket bucket)
    {
        collectedBucket = bucket;
        collectedBucket.squaresTaking++;
        hasCollected = true;
    }

    public void SetPause()
    {
        inPauseMode = true;
    }

    public void SetUnpause()
    {
        inPauseMode = false;
    }
}
