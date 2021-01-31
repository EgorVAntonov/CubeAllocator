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

    public bool CanAcceptSquare()
    {
        return currentSquare == null;
    }

    public bool CanSetSquare(Cell previousCell)
    {
        if (previousCell == null)
            return false;

        if (IsNearWithCell(previousCell) == false)
            return false;

        if (CanAcceptSquare() == false)
            return false;

        if (previousCell.currentSquare == null)
            return false;

        return true;
    }

    public bool IsNearWithCell(Cell cell)
    {
        //all cells around given one
        if (Math.Abs(gridPosition.x - cell.gridPosition.x) > 1f) return false;
        if (Math.Abs(gridPosition.y - cell.gridPosition.y) > 1f) return false;

        return true;

    }
}
