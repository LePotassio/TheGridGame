using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField]
    private CellUI cellUI;

    [SerializeField]
    private int currentValue;

    [SerializeField]
    private int previousValue;

    [SerializeField]
    private CellData cellData;

    private int rowPos;

    private int columnPos;

    //private int triggerPrecedence = 0;

    public CellData CellData
    {
        get { return cellData; }
    }

    public int RowPosition
    {
        get { return rowPos; }
        set { rowPos = value; }
    }

    public int ColumnPosition
    {
        get { return columnPos; }
        set { columnPos = value; }
    }

    private void Start()
    {
        if (cellUI)
            cellUI.UpdateUI(this);
    }

    public void AddCellValue(int val)
    {
        SetCellValue(currentValue + val);
    }

    public void SetCellValue(int newValue)
    {
        previousValue = currentValue;
        currentValue = Mathf.Clamp(newValue, cellData.MinValue, cellData.MaxValue);
        if (cellUI)
            cellUI.UpdateUI(this);
    }

    public int GetCellValue()
    {
        return currentValue;
    }

    public void TriggerCell()
    {
        List<(int, int)> actionCellShapeCoords = new List<(int, int)>();
        actionCellShapeCoords = CellShapeDB.shapes[cellData.CellActionShape].Invoke(this);
        List<Cell> potentialActionCells = GetCellsByRelativeCoords(actionCellShapeCoords);
        
        Action<Cell, Cell> action = CellActionDB.actions[cellData.CellActionType];

        // Move listener checks here
        foreach (Cell cell in potentialActionCells)
        {
            bool doAction = CellListenterDB.listeners[cell.cellData.CellActionListener].Invoke((this, cell));

            if (!doAction)
                continue;

            if (GameManager.Instance.CurrentStepActions.ContainsKey(cell)) {
                (Action<Cell, Cell>, Cell)  currentStoredAction = GameManager.Instance.CurrentStepActions[cell];

                if (currentStoredAction.Item2.cellData.ActionPrecedence < cellData.ActionPrecedence)
                {
                    GameManager.Instance.CurrentStepActions[cell] = (action, this);
                }
            }
            else
            {
                GameManager.Instance.CurrentStepActions.Add(cell, (action, this));
            }
        }

        List<(int, int)> triggerCellShapeCoords = new List<(int, int)>();
        triggerCellShapeCoords = CellShapeDB.shapes[cellData.CellTriggerShape].Invoke(this);
        List<Cell> triggerCells = GetCellsByRelativeCoords(triggerCellShapeCoords);
        foreach (Cell cell in triggerCells)
        {
            bool doTrigger = CellListenterDB.listeners[cell.cellData.CellTriggerListener].Invoke((this, cell));

            if (!doTrigger)
                continue;

            if (!GameManager.Instance.CellsToTriggerNext.Contains(cell))
                GameManager.Instance.CellsToTriggerNext.Add(cell);
        }
    }

    public List<Cell> GetCellsByExactCoords(List<(int, int)> coords)
    {
        List<Cell> cells = new List<Cell>();
        foreach (var kvp in coords)
        {
            Cell c = GameManager.Instance.Grid.GetGridCell(kvp.Item1, kvp.Item2);
            if (c)
                cells.Add(c);
        }
        return cells;
    }

    public List<Cell> GetCellsByRelativeCoords(List<(int, int)> relativeCoords)
    {
        List<(int, int)> exactCoords = new List<(int, int)>();

        foreach ((int, int) coord in relativeCoords)
        {
            exactCoords.Add((coord.Item1 + rowPos, coord.Item2 + columnPos));
        }

        List<Cell> cells = GetCellsByExactCoords(exactCoords);

        return cells;
    }
}
