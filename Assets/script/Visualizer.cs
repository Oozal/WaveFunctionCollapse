using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class Visualizer : MonoBehaviour
{
    Vector2 mousePos;
  
    Camera mainCam;
    RaycastHit2D hit;
    public Text listText;
    private void Awake()
    {
        mainCam = Camera.main;
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

            hit = Physics2D.Raycast(mousePos,-Vector3.up,11);
            if(hit)
            {
                PlaceholderIndex p = hit.collider.GetComponent<PlaceholderIndex>();
                Cell c = CellCreator.instance.cellsHolder[p.myIndex.x, p.myIndex.y];
                StringBuilder sb = new StringBuilder();
                foreach(int i in c.possibleTileList)
                {
                    sb.Append(i).Append(",");

                }
                string s = sb.ToString();
                listText.text = "{ " + s + " }";


            }
        }
    }

}
