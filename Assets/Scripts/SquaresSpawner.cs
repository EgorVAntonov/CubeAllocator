using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaresSpawner : MonoBehaviour
{
    private ColorHandler colorHandler;
    private CellsGrid grid;
    private PauseBlocker pauseBlocker;

    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private float interval;

    private float timeToSpawn;
    private float timer;

    private void Awake()
    {
        colorHandler = FindObjectOfType<ColorHandler>();
        pauseBlocker = FindObjectOfType<PauseBlocker>();
        grid = GetComponent<CellsGrid>();
    }

    private void Start()
    {
        timeToSpawn = 1f; // remake later
        timer = 0f;
    }

    private void Update()
    {
        if (timer > timeToSpawn)
        {
            if (CanSpawn())
            {
                SpawnNewSquare();
                timeToSpawn += interval;
            }
            else
            {
                Debug.Log("GAME OVER");
                Debug.Break();
            }
        }

        if (pauseBlocker.pauseMode == false)
        {
            timer += Time.deltaTime;
        }

    }

    private bool CanSpawn()
    {
        return grid.HasEmptyCell() && pauseBlocker.pauseMode == false;
    }

    private void SpawnNewSquare()
    {
        Square newSquare = Instantiate(squarePrefab).GetComponent<Square>();
        newSquare.SpawnOnCell(grid.GetRandomCell());
    }

}
