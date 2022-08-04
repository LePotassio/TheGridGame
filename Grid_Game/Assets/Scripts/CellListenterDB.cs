using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellListenterDB
{
    public static Dictionary<CellListener, Func<(Cell, Cell), bool>> listeners = new Dictionary<CellListener, Func<(Cell, Cell), bool>>()
    {
        {
            CellListener.MatchMagnitude,
            ((Cell, Cell) trigListPair) =>
            {
                Cell triggerer = trigListPair.Item1;
                Cell listener = trigListPair.Item2;
                if (triggerer.GetCellValue() == listener.GetCellValue() + triggerer.ListenerMagnitude)
                    return true;
                return false;
            }
        }
    };
}
