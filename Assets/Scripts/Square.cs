using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SquareType
{
    Queen, Rook, Bishop
}

public class Square : MonoBehaviour, IPauseInteractor
{
    //square type
    private SquareType moveType;
    [SerializeField] private GameObject rookMarks;
    [SerializeField] private GameObject bishopMarks;
    [SerializeField] SquareType[] typesChances;

    //color
    private ColorHandler colorHandler;
    private int colorIndex;

    //comonents
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private GameObject spriteTint;
    private Animator animator;
    private BucketInteractor interactor;
    private ScoreSource source;

    private Cell ownCell;
    private bool isOnBoard;

    public delegate void SqareBreak();
    public event SqareBreak OnSquareBreak;

    private void Start()
    {
        colorHandler = FindObjectOfType<ColorHandler>();

        animator = GetComponent<Animator>();
        interactor = GetComponent<BucketInteractor>();
        source = GetComponent<ScoreSource>();
    }

    public void SetupSquare()
    {
        colorIndex = Random.Range(0, colorHandler.colors.Length);
        sprite.color = colorHandler.colors[colorIndex];
        SetSquareType();
    }

    private void SetSquareType()
    {
        moveType = typesChances[Random.Range(0, typesChances.Length)];
        SetTypeMarks(moveType);
    }

    private void SetTypeMarks(SquareType type)
    {
        switch (moveType)
        {
            case SquareType.Rook:
                rookMarks.SetActive(true);
                bishopMarks.SetActive(false);
                break;
            case SquareType.Bishop:
                rookMarks.SetActive(false);
                bishopMarks.SetActive(true);
                break;
            case SquareType.Queen:
                rookMarks.SetActive(true);
                bishopMarks.SetActive(true);
                break;
            default:
                break;
        }
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
            if (OnSquareBreak != null)
            {
                OnSquareBreak.Invoke();
                OnSquareBreak = null;
            }
        }
    }

    public void CollectSquare(Bucket collector)
    {
        UnsetTint();
        source.GetScore(collector.scoreMultiplicator);
        interactor.StartAnimationWithBucket(collector);
    }

    public int GetColorIndex()
    {
        return colorIndex;
    }

    public SquareType GetMoveType()
    {
        return moveType;
    }

    public void SetTint()
    {
        spriteTint.SetActive(true);
    }

    public void UnsetTint()
    {
        spriteTint.SetActive(false);
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
