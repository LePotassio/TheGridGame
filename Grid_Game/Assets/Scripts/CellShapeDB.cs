using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellShapeDB
{
    public static Dictionary<CellTriggerShape, Func<Cell, List<(int, int)>>> shapes = new Dictionary<CellTriggerShape, Func<Cell, List<(int,int)>>>()
    {
        {
            CellTriggerShape.Square,
            (Cell cell) =>
            {
                List<(int, int)> cellLocations = new List<(int, int)>();
                cellLocations = new List<(int, int)>
                {
                    (-1, -1),
                    (-1, 0),
                    (-1, 1),
                    (0, -1),
                    (0, 1),
                    (1, -1),
                    (1, 0),
                    (1, 1),
                };
                return cellLocations;
            }
        }
    };
}
