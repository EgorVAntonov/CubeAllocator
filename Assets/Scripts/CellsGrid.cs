using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BucketSide { top, right, bottom, left }

public class CellsGrid : MonoBehaviour
{
    public const int HEIGHT = 6;
    public const int WIDHT = 6;

    private Cell[,] cells = new Cell[WIDHT, HEIGHT];

    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private GameObject bucketPrefab;

    private BucketsPool allBuckets;

    void Awake()
    {
        allBuckets = GetComponent<BucketsPool>();
        allBuckets.SetArraysLengths(WIDHT, HEIGHT);
        CreateGrid(WIDHT, HEIGHT);
    }

    private void CreateGrid(int width, int height)
    {
        cells = new Cell[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 cellPosition = GetPositionForCell(i, j);
                GameObject currentCell = Instantiate(cellPrefab, cellPosition, Quaternion.identity, transform) as GameObject;

                currentCell.name = "cell[" + i.ToString() + ", " + j.ToString() + "]";
                cells[i, j] = currentCell.GetComponent<Cell>();
                cells[i, j].gridPosition = new Vector2(i, j);

                if (i == 0)                 //left
                    CreateBucket(i, j, cellPosition, BucketSide.left);
                else if (i == WIDHT - 1)    //right
                    CreateBucket(i, j, cellPosition, BucketSide.right);

                if (j == 0)                 //bottom
                    CreateBucket(i, j, cellPosition, BucketSide.bottom);
                else if (j == HEIGHT - 1)   //top
                    CreateBucket(i, j, cellPosition, BucketSide.top);
            }
        }
        
    }

    private Vector3 GetPositionForCell(int x, int y)
    {
        return new Vector3(x - (WIDHT / 2f) + 0.5f, y - (HEIGHT / 2f) + 0.5f, 0f);
    }

    private void CreateBucket(int i, int j, Vector3 cellPosition, BucketSide type)
    {
        Bucket newBucket = Instantiate(bucketPrefab).GetComponent<Bucket>();
        Vector3 setupPos = cellPosition;
        Quaternion setupRot = new Quaternion();

        switch (type)
        {
            case BucketSide.top:

                allBuckets.SetBucketToRow(newBucket, BucketSide.top);
                setupPos = cellPosition + Vector3.up;
                setupRot = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                break;
            case BucketSide.right:

                allBuckets.SetBucketToRow(newBucket, BucketSide.right);
                setupPos += Vector3.right;
                setupRot = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                break;
            case BucketSide.bottom:

                allBuckets.SetBucketToRow(newBucket, BucketSide.bottom);
                setupPos += Vector3.down;
                setupRot = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                break;
            case BucketSide.left:

                allBuckets.SetBucketToRow(newBucket, BucketSide.left);
                setupPos += Vector3.left;
                setupRot = Quaternion.Euler(new Vector3(0f, 0f, 270f));

                break;
            default:
                break;
        }
        newBucket.SetupBucket(cells[i, j], setupPos, setupRot);
    }

    public bool HasEmptyCell()
    {
        for (int i = 0; i < WIDHT; i++)
        {
            for (int j = 0; j < HEIGHT; j++)
            {
                if (cells[i, j].currentSquare == null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    public Cell GetRandomCell()
    {
        return cells[Random.Range(0, WIDHT), Random.Range(0, HEIGHT)];
    }

}