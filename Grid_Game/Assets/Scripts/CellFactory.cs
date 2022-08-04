using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellType { DecrementDie }
public class CellFactory
{
    public static Dictionary<CellType, CellData> cellPresets = new Dictionary<CellType, CellData>()
    {
        { CellType.DecrementDie, new CellData()
            {
               CellActionType = CellActionType.AddMagnitude,
               CellActionShape = CellTriggerShape.Square,
               CellTriggerShape = CellTriggerShape.Square,
               CellActionListener = CellListener.MatchMagnitude,
               CellTriggerListener = CellListener.MatchMagnitude,

               MinValue = 0,
               MaxValue = 6,

               Magnitude = -1,
               ListenerMagnitude = 0,

               ActionPrecedence = 0,

               PlayerTriggerable = true,

               CellType = CellType.DecrementDie,
            }
        },
    };

    public static void SetCell(Cell cell, int val, CellType cellType)
    {
        SetCellData(cell, cellPresets[cellType]);
        cell.SetCellValue(val);
    }

    private static void SetCellData(Cell cell, CellData newCellData)
    {
        CellData cellData = cell.CellData;

        cellData.CellActionType = newCellData.CellActionType;
        cellData.CellActionShape = newCellData.CellActionShape;
        cellData.CellTriggerShape = newCellData.CellTriggerShape;
        cellData.CellActionListener = newCellData.CellActionListener;
        cellData.CellTriggerListener = newCellData.CellTriggerListener;

        cellData.MinValue = newCellData.MinValue;
        cellData.MaxValue = newCellData.MaxValue;

        cellData.Magnitude = newCellData.Magnitude;
        cellData.ListenerMagnitude = newCellData.ListenerMagnitude;

        cellData.ActionPrecedence = newCellData.ActionPrecedence;

        cellData.PlayerTriggerable = newCellData.PlayerTriggerable;

        cellData.CellType = newCellData.CellType;
    }
}
