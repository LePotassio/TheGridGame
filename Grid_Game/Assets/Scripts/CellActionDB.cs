using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellActionDB
{
    public static Dictionary<CellActionType, Action<Cell, Cell>> actions = new Dictionary<CellActionType, Action<Cell, Cell>>()
    {
        {
            CellActionType.AddMagnitude,
            (Cell senderCell, Cell recieverCell) =>
            {
                if (recieverCell)
                    recieverCell.AddCellValue(senderCell.Magnitude);
            }
        }
    };
}
