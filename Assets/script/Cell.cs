using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell
{
    public Vector2Int myIndex;
    public Vector3 position;
    public List<int> possibleTileList = new List<int> { 0, 1, 2, 3, 4, 5, 6,7};
    Tile myTile;
    int[] tempPossibleTileList;
    public List<Cell> neighbourCells = new List<Cell>();
    public bool collapsed;
    public bool zeroNeighbourPossibility;
    Cell leftNeighbour, rightNeighbour, buttomNeighbour, topNeighbour;
    public IDictionary<Vector2, Cell> neighborsDictionary = new Dictionary<Vector2, Cell>();

     public void TryCollapseNeighbour()
    {
        foreach (int p in possibleTileList)
        {
            Tile t = TileMapper.instance.MapTile(p);
            leftNeighbour = neighborsDictionary[Vector2.left];
            if (leftNeighbour!=null)
            {
                if(leftNeighbour.possibleTileList.Count>1)
                {
                    zeroNeighbourPossibility = leftNeighbour.TrimmingProbability(t.leftNeighbour);
                    if(zeroNeighbourPossibility)
                    {
                        
                        break;
                    }
                }
                
            }

            rightNeighbour = neighborsDictionary[Vector2.right];
            if (rightNeighbour!=null)
            {
                if(rightNeighbour.possibleTileList.Count>1)
                {
                    zeroNeighbourPossibility= rightNeighbour.TrimmingProbability(t.RightNeighbour);
                    if (zeroNeighbourPossibility)
                    {
                        break;
                    }
                }
               

            }

            buttomNeighbour = neighborsDictionary[Vector2.down]; 
            if (buttomNeighbour!=null)
            {
                if(buttomNeighbour.possibleTileList.Count>1)
                {
                    zeroNeighbourPossibility = buttomNeighbour.TrimmingProbability(t.ButtomNeighbour);
                    if (zeroNeighbourPossibility)
                    {
                  
                        break;
                    }
                }
               

            }

            topNeighbour =neighborsDictionary[Vector2.up];
            if (topNeighbour!=null)
            {
                if(topNeighbour.possibleTileList.Count>1)
                {
                    zeroNeighbourPossibility = topNeighbour.TrimmingProbability(t.TopNeighbour);
                    if (zeroNeighbourPossibility)
                    {
                        break;
                    }

                }

            }

        }
    }
    public void FindNeighbours()
    {
        if (myIndex.y > 0)
        {
            Cell leftNeighbour = CellCreator.instance.cellsHolder[myIndex.x, myIndex.y - 1];
            neighbourCells.Add(leftNeighbour);
            neighborsDictionary.Add(Vector2.left, leftNeighbour);
            
        }
        else
            neighborsDictionary.Add(Vector2.left, null);


        if (myIndex.y < CellCreator.instance.GetNumTiles - 1)
        {
            Cell rightNeighbour = CellCreator.instance.cellsHolder[myIndex.x, myIndex.y + 1];
            neighbourCells.Add(rightNeighbour);
            neighborsDictionary.Add(Vector2.right, rightNeighbour);


        }
        else
            neighborsDictionary.Add(Vector2.right, null);

        if (myIndex.x > 0)
        {
            Cell buttomNeighbour = CellCreator.instance.cellsHolder[myIndex.x - 1, myIndex.y];
            neighbourCells.Add(buttomNeighbour);
            neighborsDictionary.Add(Vector2.down, buttomNeighbour);
           

        }
        else
            neighborsDictionary.Add(Vector2.down, null);


        if (myIndex.x < CellCreator.instance.GetNumTiles - 1)
        {
            Cell topNeighbour = CellCreator.instance.cellsHolder[myIndex.x + 1, myIndex.y];
            neighbourCells.Add(topNeighbour);
            neighborsDictionary.Add(Vector2.up, topNeighbour);

        }
        else
            neighborsDictionary.Add(Vector2.up, null);
    }

    public void FinalCollapse(int id)
    {
        //myTile = TileMapper.instance.MapTile(id);
        collapsed = true;
        //CellCreator.instance.FillSingleTile(id, position);
        possibleTileList.Clear();
        possibleTileList.Add(id);
        
    }

    public bool TrimmingProbability(List<int> comparisionList)
    {
        tempPossibleTileList = new int[possibleTileList.Count];
        tempPossibleTileList = possibleTileList.ToArray();
        foreach(int tilenum in tempPossibleTileList)
        {
            if(!comparisionList.Contains(tilenum))
            {
                
                    possibleTileList.Remove(tilenum);
            }
        }
        if (possibleTileList.Count == 0)
        {
            return true;
        }
        else
            return false;
    }

    public void ResetProbability()
    {
        collapsed = false;
        possibleTileList = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        foreach(Vector2 v in neighborsDictionary.Keys)
        {
            Cell calcualtedCell = neighborsDictionary[v];
            if (calcualtedCell != null)
            {
                if (calcualtedCell.possibleTileList.Count==1 && calcualtedCell.collapsed)
                {
                    Tile t = TileMapper.instance.MapTile(neighborsDictionary[v].possibleTileList[0]);
                    if (v == Vector2.left)
                    {
                        TrimmingProbability(t.RightNeighbour);
                    }
                    else if (v == Vector2.right)
                    {
                        TrimmingProbability(t.leftNeighbour);
                    }

                    else if (v == Vector2.up)
                    {
                        TrimmingProbability(t.ButtomNeighbour);
                    }
                    else if (v == Vector2.down)
                    {
                        TrimmingProbability(t.TopNeighbour);
                    }


                }
            } 
        }
        
    }

    public void ResetAllNeighbours()
    {
        foreach(Cell c in neighborsDictionary.Values)
        {
            if(c!=null && c.collapsed == false )
                c.ResetProbability();
        }
    }
  
}
