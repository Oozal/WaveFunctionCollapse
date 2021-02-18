using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveFunctionCollapse : MonoBehaviour
{
    int iteration = 0;
    Cell currentCell;
    [SerializeField]
    private int iterationLimit;
    int minNum = 10;
    Cell cellToTrim = new Cell();
    public bool fullyCollapsed;
    public Stack<Cell> cellsStack = new Stack<Cell>();
    public void InitializeWaveFunctionCollapse()
    {
        
        ChooseRandomCell();
        StartCollapsing(currentCell);
        if(fullyCollapsed)
        {
            CellCreator.instance.FillTile();
            Debug.Log("Done");

        }
        else
        {
            if (iteration < iterationLimit)
                InitializeWaveFunctionCollapse();
            else
            {
                Debug.Log("Iteration exceeded");
                CellCreator.instance.FillTile();
            }
                

        }
        
    }

    void ChooseRandomCell ()
    {
        int numTiles = CellCreator.instance.GetNumTiles;

        int indexR = Random.Range(0, numTiles);
        int indexC = Random.Range(0, numTiles);
        Debug.LogError("first cell index: " + indexR + ", " + indexC);
        currentCell = new Cell();
        currentCell = CellCreator.instance.cellsHolder[indexR, indexC];
    }

    public void Recalcualte()
    {
        StartCollapsing(currentCell);
    }


    void StartCollapsing(Cell c)
    {
        if (iteration < iterationLimit)
        {
            int count = 0;
            List<int> tempList = new List<int>();
            foreach (int i in c.possibleTileList)
            {
                tempList.Add(i);
            }

            for (int i = 0; i < c.possibleTileList.Count; i++)
            {
                int randomIndex = Random.Range(0, (int)tempList.Count);
                int selectedTile = tempList[randomIndex];
                tempList.Remove(selectedTile);
                c.FinalCollapse(selectedTile);
                c.TryCollapseNeighbour();
                if (c.zeroNeighbourPossibility)
                {
                    c.ResetProbability();
                    c.ResetAllNeighbours();
                    c.zeroNeighbourPossibility = false;
                    continue;
                }
                minNum = 10;
                Cell minEntropyCell = new Cell();
                foreach (Cell cell in CellCreator.instance.cellsHolder)
                {

                    int possibleTileCount = cell.possibleTileList.Count;
                    if (possibleTileCount >= 1 && !cell.collapsed)
                    {
                        count += 1;
                        if (possibleTileCount < minNum)
                        {
                            minNum = possibleTileCount;
                            minEntropyCell = cell;
                        }
                    }

                }
                if (count == 0)
                {
                    fullyCollapsed = true;
                }
                if (fullyCollapsed)
                {
                    //Debug.LogError("Fully Collapsed");
                }
                if (!fullyCollapsed)
                {
                    iteration += 1;
                    StartCollapsing(minEntropyCell);
                }
                if (!fullyCollapsed)
                {
                    c.ResetProbability();
                    c.ResetAllNeighbours();
                }
                else
                {
                    break;
                }

            }
            return;
        }

    }
        
           
}
