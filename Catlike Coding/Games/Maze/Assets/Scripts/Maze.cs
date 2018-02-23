﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    private MazeCell[,] cells;

    public IntVector2 size;
    public MazeCell cellPrefab;
    public MazePassage passagePrefab;
    public MazeWall[] wallPrefabs;
    public MazeDoor doorPrefab;

    public float generationStepDelay;

    [Range(0f, 1f)]
    public float doorProbability;

    public IntVector2 RandomCoordinates
    {
        get
        {
            return new IntVector2(Random.Range(0,size.x),Random.Range(0,size.z));
        }
    }

    public MazeCell GetCell (IntVector2 coordinate)
    {
        return cells[coordinate.x, coordinate.z];
    }

    public IEnumerator Generate ()
    {
		WaitForSeconds delay = new WaitForSeconds(generationStepDelay);
		cells = new MazeCell[size.x, size.z];
        //IntVector2 coordinates = RandomCoordinates;
        //while (ContainsCoordinates(coordinates) && GetCell(coordinates) == null) {
        //	yield return delay;
        //	CreateCell(coordinates);
        //	coordinates += MazeDirections.RandomValue.ToIntVector2();
        List<MazeCell> activeCells = new List<MazeCell>();
        DoFirstGenerationStep(activeCells);
        while (activeCells.Count > 0)
        {
            yield return delay;
            DoNextGenerationStep(activeCells);
        }
	}

    private void DoFirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }

    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];
        if(currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }
        MazeDirection direction = currentCell.RandomUninitializedDirection;
        IntVector2 coordinates = currentCell.coordinates + direction.ToIntVector2();
        if(ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if(neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
                //activeCells.RemoveAt(currentIndex);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
            //activeCells.RemoveAt(currentIndex);
        }
    }

    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefabs[Random.Range(0,wallPrefabs.Length)]) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if(otherCell != null)
        {
            wall = Instantiate(wallPrefabs[Random.Range(0,wallPrefabs.Length)]) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }

    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage prefab = Random.value < doorProbability ? doorPrefab : passagePrefab;
        MazePassage passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(prefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }

    public bool ContainsCoordinates(IntVector2 coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.z >= 0 && coordinate.z < size.z;
    }   

    private MazeCell CreateCell(IntVector2 coordinates)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[coordinates.x, coordinates.z] = newCell;
        newCell.coordinates = coordinates;
        newCell.name = "Maze Cell " + coordinates.x + ", " + coordinates.z;
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(coordinates.x - size.x * 0.5f + 0.5f, 0f, coordinates.z - size.z * 0.5f + 0.5f);
        return newCell;
    }
}
