using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquaresSpawner : MonoBehaviour
{
    private ColorHandler colorHandler;
    private CellsGrid grid;
    private PauseBlocker pauseBlocker;
    private GameLoop gameLoop;

    [SerializeField] private GameObject squarePrefab;
    [SerializeField] private float interval;

    private float timeToSpawn;
    private float timer;

    //public bool forTesting = false;

    private void Awake()
    {
        colorHandler = FindObjectOfType<ColorHandler>();
        pauseBlocker = FindObjectOfType<PauseBlocker>();
        gameLoop = FindObjectOfType<GameLoop>();
        grid = GetComponent<CellsGrid>();
    }

    private void Start()
    {
        timeToSpawn = 1f; 
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
                /* manual spawning for testing
                if (forTesting == true)
                {
                    forTesting = false;
                    SpawnNewSquare();
                    timeToSpawn += interval;
                }
                */
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
        return pauseBlocker.pauseMode == false;
    }

    private void SpawnNewSquare()
    {
        Square newSquare = Instantiate(squarePrefab).GetComponent<Square>();
        newSquare.OnSquareBreak += gameLoop.SetGameOver;
        newSquare.SpawnOnCell(grid.GetRandomCell());
    }

    public void SetNewInterval(float value)
    {
        interval = value;
    }

}
