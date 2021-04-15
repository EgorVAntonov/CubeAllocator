using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SquareMover))]
public class Cell : MonoBehaviour
{
    private CellsGrid grid;
    public Vector2 gridPosition;

    public Square currentSquare;
    
    public event Action OnSquareSet;

    private void Start()
    {
        grid = FindObjectOfType<CellsGrid>();
    }

    public void SetSquare(Square square)
    {
        square.transform.position = transform.position;
        currentSquare = square;
        if (OnSquareSet != null)
            OnSquareSet();
    }

    public void RemoveSquare()
    {
        currentSquare = null;
    }

    public bool CanMoveSquareFrom(Cell previousCell)
    {
        if (previousCell == null)
            return false;

        if (previousCell.currentSquare == null)
            return false;

        if (CanAcceptSquare() == false)
            return false;

        if (SquareCanMoveOnThisCell(previousCell, previousCell.currentSquare.GetMoveType()) == false)
            return false;

        return true;
    }

    public bool SquareCanMoveOnThisCell(Cell cell, SquareType moveType)
    {
        float xDiffrence = Mathf.Abs(gridPosition.x - cell.gridPosition.x);
        float yDiffrence = Mathf.Abs(gridPosition.y - cell.gridPosition.y);
        switch (moveType)
        {
            case SquareType.Rook:
                //aligned cells
                if ((xDiffrence == 1f) && (yDiffrence == 0f)||
                    (xDiffrence == 0f) && (yDiffrence == 1f))
                    return true;
                break;
            case SquareType.Bishop:
                //diagonal cells
                if ((xDiffrence == 1f) && (yDiffrence == 1f))
                    return true;
                break;
            case SquareType.Queen:
                //all cells around
                if ((xDiffrence == 1f) && (yDiffrence == 0f) ||
                    (xDiffrence == 0f) && (yDiffrence == 1f) ||
                    (xDiffrence == 1f) && (yDiffrence == 1f))
                    return true;
                break;
            default:
                break;
        }
        return false;
    }

    public bool CanAcceptSquare()
    {
        return currentSquare == null;
    }
}
