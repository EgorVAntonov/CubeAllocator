using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SquareMover : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler
{
    private CellsGrid grid;
    private Cell cell;

    public Text testText;
    public static Cell[] currentInteractingCells = new Cell[10];

    private void Start()
    {
        grid = FindObjectOfType<CellsGrid>();
        cell = GetComponent<Cell>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
       // Debug.Log("pointer " + eventData.pointerId + " enter " + gameObject.name);
        int pointerID = eventData.pointerId;

        if (pointerID < 0) return;

        Cell previousCell = currentInteractingCells[pointerID];

        if (cell.CanMoveSquareFrom(previousCell))
        {
            cell.SetSquare(previousCell.currentSquare);
            previousCell.RemoveSquare();
            currentInteractingCells[pointerID] = cell;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.pointerId < 0) return;

        // Debug.Log("pointer " + eventData.pointerId + " down on  " + gameObject.name);
        currentInteractingCells[eventData.pointerId] = cell;
        if (cell.currentSquare != null)
        {
            cell.currentSquare.SetTint();
        }
        
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerId < 0) return;

        // Debug.Log("pointer " + eventData.pointerId + " up from  " + gameObject.name);

        if (currentInteractingCells[eventData.pointerId].currentSquare != null)
        {
            currentInteractingCells[eventData.pointerId].currentSquare.UnsetTint();
        }
        currentInteractingCells[eventData.pointerId] = null;
    }
    
}
