using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Square : MonoBehaviour, IPauseInteractor
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject spriteTint;

    private Animator animator;
    private ColorHandler colorHandler;
    private Cell ownCell;
    private bool isOnBoard;
    private int colorIndex;

    private void Start()
    {
        colorHandler = FindObjectOfType<ColorHandler>();
        animator = GetComponent<Animator>();
    }

    public void SetupSquare()
    {
        colorIndex = Random.Range(0, colorHandler.colors.Length);
        sprite.color = colorHandler.colors[colorIndex];
    }

    public void SpawnOnCell(Cell cell)
    {
        isOnBoard = false;
        ownCell = cell;
        transform.position = cell.transform.position; 
        GetComponent<Animator>().SetTrigger("Fall");
    }

    public void TrySetOnBoard()
    {
        SetupSquare();
        if (ownCell.CanAcceptSquare())
        {
            isOnBoard = true;
            ownCell.SetSquare(this);
        }
        else
        {
            Debug.Log("square break");
            Debug.Break();
        }
    }

    public int GetColorIndex()
    {
        return colorIndex;
    }

    public void SetTint(bool state)
    {
        spriteTint.SetActive(state);
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
