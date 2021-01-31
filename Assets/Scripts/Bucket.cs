using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bucket : MonoBehaviour, IPooledObject, IPauseInteractor
{
    private ColorHandler colorHandler;
    private Animator animator;

    private int colorIndex;
    public bool canBeChosenForActivation;
    public bool canAcceptSquare;

    public Cell closestCell;
    public int squaresTaking;


    public void SetupBucket(Cell closestCell, Vector3 setupPos, Quaternion setupRot)
    {
        transform.SetPositionAndRotation(setupPos, setupRot);

        this.closestCell = closestCell;
        closestCell.OnSquareSet += TryAcceptSquare;

        colorHandler = FindObjectOfType<ColorHandler>();
        animator = GetComponent<Animator>();

        canBeChosenForActivation = true;
        canAcceptSquare = false;
    }

    public void SetRandomColor()
    {
        colorIndex = Random.Range(0, colorHandler.colors.Length);
        foreach (SpriteRenderer spriteRenderer in GetComponentsInChildren<SpriteRenderer>())
        {
            spriteRenderer.color = colorHandler.colors[colorIndex];
        }
    }

    public void TryAcceptSquare()
    {
        if (closestCell.currentSquare == null) return;
        if (canAcceptSquare == false) return;

        if (closestCell.currentSquare.GetColorIndex() == colorIndex)
        {
            Debug.Log("square succsesfully delete");
            closestCell.currentSquare.SetTint(false);
            closestCell.currentSquare.GetComponent<BucketInteractor>().StartAnimationWithBucket(this);//make pool later
            closestCell.currentSquare = null;
        }
    }

    public void GetFromPool()
    {
        SetRandomColor();
        canBeChosenForActivation = false;
        squaresTaking = 0;
        animator.SetTrigger("Show");
    }

    public void ReturnToPool()
    {
        canBeChosenForActivation = true;
        animator.SetTrigger("Hide");
    }

    private void OnEnable()
    {
        if (closestCell != null)
            closestCell.OnSquareSet += TryAcceptSquare;
    }

    private void OnDisable()
    {
        if (closestCell != null)
            closestCell.OnSquareSet -= TryAcceptSquare;
        
    }

    public void SetPause()
    {
        animator.speed = 0f;
    }

    public void SetUnpause()
    {
        animator.speed = 1f;
    }
}
