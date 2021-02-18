using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class CellCreator : MonoBehaviour
{
    [SerializeField]
    private int numTiles;
    [SerializeField]
    private float offset;
    public Cell[,] cellsHolder;
    PlaceholderIndex[,] placeHolders;
    Cell cell;
    public static CellCreator instance;
    [SerializeField]
    private PlaceholderIndex tilePlaceHolder;

    [SerializeField]
    private WaveFunctionCollapse waveFunctionCollapse;

    public int GetNumTiles {
        get
        {
            return numTiles;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        cellsHolder = new Cell[numTiles, numTiles];
        placeHolders = new PlaceholderIndex[numTiles, numTiles];
        for(int i=0;i<numTiles;i++)
        {
            for(int j=0;j<numTiles;j++)
            {
                Vector3 currentPos = new Vector3(j * offset,0, i * offset);
                cell = new Cell();
                cell.position = currentPos;
                cell.myIndex = new Vector2Int(i, j);
                cellsHolder[i, j] = cell;
                PlaceholderIndex placeholder = Instantiate(tilePlaceHolder, currentPos, Quaternion.identity);
                placeholder.myIndex = new Vector2Int(i, j);
                placeHolders[i, j] = placeholder;

            }
        }
        foreach(Cell c in cellsHolder)
        {
            c.FindNeighbours();
        }
        waveFunctionCollapse.InitializeWaveFunctionCollapse();
    }
    
    public void FillTile()
    {
        foreach (Cell c in cellsHolder)
        {
            
            List<int> possibilityList = c.possibleTileList;
            StringBuilder sb = new StringBuilder();
            foreach(int n in possibilityList)
            {
                sb.Append(n).Append(", ");
            }
            string result = sb.ToString();
            Debug.Log(c.myIndex + " : " + c.possibleTileList.Count+" { "+result+" }");
            //if (possibilityList.Count==1)
            //{
                Vector3 instantiationPos = c.position;
                Tile t = TileMapper.instance.MapTile(possibilityList[0]);
                Instantiate(t, instantiationPos,t.transform.rotation);
            //}
            //else if(possibilityList.Count==2)
            //{
            //    Vector2 instantiationPos = c.position;
            //    int randomIndex = UnityEngine.Random.Range(0, possibilityList.Count);
            //    Tile t = TileMapper.instance.MapTile(possibilityList[randomIndex]);
            //    Instantiate(t, instantiationPos, Quaternion.identity);

            //}
        }
    }

    public void FillSingleTile(int id,Vector2 pos)
    {
        Tile t = TileMapper.instance.MapTile(id);
        Instantiate(t, pos, Quaternion.identity);
    }
}
