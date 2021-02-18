using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TileMapper : MonoBehaviour
{
    public static TileMapper instance;

    //public Tile grassTile_1, grassTile_2, VerticalRoadTile, horizontalRoadTile, intersectionTile;
    public Tile grassTile, Horizontal, vertical, cornerB_L, cornerT_R, cornerL_T, cornerB_R,hole;

    private void Awake()
    {
        instance = this;
    }
    public Tile MapTile(int index)
    //{
    //    switch (index)
    //    {
    //        case (0):
    //            return grassTile_1;
    //        case (1):
    //            return grassTile_2;
    //        case (2):
    //            return VerticalRoadTile;
    //        case (3):
    //            return horizontalRoadTile;
    //        case (4):
    //            return intersectionTile;

    //    }
    //    return null;

    {
        switch (index)
        {
            case (0):
                return grassTile;
            case (1):
                return Horizontal;
            case (2):
                return vertical;
            case (3):
                return cornerB_L;
            case (4):
                return cornerT_R;
            case (5):
                return cornerL_T;
            case (6):
                return cornerB_R;

            case (7):
                return hole;
        }
        return null;


    }
}
