using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Prefab for adding the tiles
    public GameObject tilePrefab0, tilePrefab3, tilePrefab5, tilePrefab6, tilePrefab9, tilePrefab10, tilePrefab12;
    public int gridSize = 4;
    public int leftExit, rightExit;
    private Dictionary<int, GameObject> tilePrefabs = new Dictionary<int, GameObject>();
    private int[,] originalGrid;
    private int[,] currentGrid;
    private Stack<int> tileSequence;


    // Start is called before the first frame update
    void Start()
    {
        // I want a dictionary to map the number to the prefab. Hope this will make sense later.
        tilePrefabs.Add(0, tilePrefab0);
        tilePrefabs.Add(3, tilePrefab3);
        tilePrefabs.Add(5, tilePrefab5);
        tilePrefabs.Add(6, tilePrefab6);
        tilePrefabs.Add(9, tilePrefab9);
        tilePrefabs.Add(10, tilePrefab10);
        tilePrefabs.Add(12, tilePrefab12);
        SetupTileSequence();
        DisplayOriginalGrid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddTile(float x_pos, float y_pos)
    {
        GameObject selectedPrefab = tilePrefab0;
        // This just randomly selects a prefab. Placeholder code!
        if (!tilePrefabs.TryGetValue(Random.Range(3, 13), out selectedPrefab)) selectedPrefab = tilePrefab0;
        Instantiate(selectedPrefab, new Vector3(x_pos, y_pos), Quaternion.identity);
    }

    private void SetupTileSequence()
    {
        originalGrid = new int[gridSize, gridSize];
        int[] transitions = new int[gridSize + 1];
        // I want to create a solvable puzzle, so the original solution (there will be many more) will be created in an easy way.
        // Going only from left to right and allowing the pipe to go up and down in between. Such a pipe can be defined by just 
        // the y values at the transitions from one column to the next.
        for (int i = 0; i < gridSize + 1; i++)
        {
            transitions[i] = Random.Range(0, gridSize);
        }
        leftExit = transitions[0];
        rightExit = transitions[gridSize];
        // Now based on the randomly generated transitions, we determine which tile must be placed at every point of the grid.
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (transitions[j] > i)
                {
                    if (transitions[j + 1] > i)
                    {
                        originalGrid[i, j] = Random.Range(3, 13);
                    }
                    else if (transitions[j + 1] == i)
                    {
                        originalGrid[i, j] = 6;
                    } else
                    {
                        originalGrid[i, j] = 5;
                    }
                }
                else if (transitions[j] == i)
                {
                    if (transitions[j + 1] > i)
                    {
                        originalGrid[i, j] = 12;
                    }
                    else if (transitions[j + 1] == i)
                    {
                        originalGrid[i, j] = 10;
                    }
                    else
                    {
                        originalGrid[i, j] = 9;
                    }
                }
                else
                {
                    if (transitions[j + 1] > i)
                    {
                        originalGrid[i, j] = 5;
                    }
                    else if (transitions[j + 1] == i)
                    {
                        originalGrid[i, j] = 3;
                    }
                    else
                    {
                        originalGrid[i, j] = Random.Range(3, 13);
                    }
                }
            }
        } //end for loop filling the grid with integers representing the tiles
        // Put them in a stack randomly, just taking from the top so it will be possible to reconstruct the pipe maze.
        tileSequence = new Stack<int>();
        int[] tops = new int[gridSize]; 
        while (tileSequence.Count < gridSize*gridSize)
        {
            int column = Random.Range(0, gridSize);
            if (tops[column]<gridSize)
            {
                tileSequence.Push(originalGrid[tops[column], column]);
                tops[column]++;
                Debug.Log("" + tileSequence.Count + ". " + tileSequence.Peek());
            }
        }
    }

    private void DisplayOriginalGrid() { 
        // Now to see how it all worked out, place the generated grid in the game area. Remove this later.
        // Create the outer walls (also out of tiles because possibly easier)
        GameObject selectedPrefab = tilePrefab0;
        for (int i = 0; i < gridSize; i ++)
        {
            // Left Wall
            if  (leftExit == i)
            {
                selectedPrefab = tilePrefab10;
            }
            else
            {
                selectedPrefab = tilePrefab0;
            }
            Instantiate(selectedPrefab, new Vector3(0.65f * (-1.0f - 0.5f * (gridSize - 1)), gridSize - i - 1.0f), Quaternion.identity);
            // Right wall
            if (rightExit == i)
            {
                selectedPrefab = tilePrefab10;
            }
            else
            {
                selectedPrefab = tilePrefab0;
            }
            Instantiate(selectedPrefab, new Vector3(0.65f * (1.0f + 0.5f * (gridSize - 1)), gridSize - i - 1.0f), Quaternion.identity);
            // And now the real deal, the contents of the grid
            for (int j = 0; j < gridSize; j++)
            {
                // Print for debugging purposes
                // Debug.Log(originalGrid[i, j]);
                // And now place the tiles
                if (!tilePrefabs.TryGetValue(originalGrid[i, j], out selectedPrefab)) selectedPrefab = tilePrefab0;
                Instantiate(selectedPrefab, new Vector3(0.65f*(-0.5f * (gridSize - 1) + j), gridSize - i - 1.0f), Quaternion.identity);
            }
        }
        
    }

}
