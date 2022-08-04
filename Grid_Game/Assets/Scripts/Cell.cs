using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellActionType { AddMagnitude }
public enum CellTriggerShape { Square, Cross, List }
public enum CellListener { MatchMagnitude }
public class Cell : MonoBehaviour
{
    [SerializeField]
    private CellUI cellUI;

    [SerializeField]
    private CellActionType cellActionType; // What it does to other cells or itself

    [SerializeField]
    private CellTriggerShape cellActionShape; // Which cells it does it to

    [SerializeField]
    private CellTriggerShape cellTriggerShape;  // Which cells to do next

    [SerializeField]
    private CellListener cellActionListener; // What things must the cell be to do the action to it

    [SerializeField]
    private CellListener cellTriggerListener; // What things must the cell be to trigger it

    [SerializeField]
    private int currentValue;

    [SerializeField]
    private int previousValue;

    [SerializeField]
    private int magnitude = -1;

    [SerializeField]
    private int listenerMagnitude = 0;

    [SerializeField]
    private int actionPrecedence = 0;

    private int rowPos;

    private int columnPos;

    //private int triggerPrecedence = 0;

    public CellActionType CellActionType
    {
        get { return cellActionType; }
        set { cellActionType = value; }
    }

    public CellTriggerShape CellActionShape
    {
        get { return cellActionShape; }
        set { cellActionShape = value; }
    }

    public CellTriggerShape CellTriggerShape
    {
        get { return cellTriggerShape; }
        set { cellTriggerShape = value; }
    }

    public CellListener CellActionListener
    {
        get { return cellActionListener; }
        set { cellActionListener = value; }
    }

    public CellListener CellTriggerListener
    {
        get { return cellTriggerListener; }
        set { cellTriggerListener = value; }
    }

    public int Magnitude
    {
        get { return magnitude; }
        set { magnitude = value; }
    }

    public int ListenerMagnitude
    {
        get { return listenerMagnitude; }
        set { listenerMagnitude = value; }
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
        cellUI.UpdateUI(this);
    }

    public void AddCellValue(int val)
    {
        SetCellValue(currentValue + val);
    }

    public void SetCellValue(int newValue)
    {
        previousValue = currentValue;
        currentValue = newValue;
        cellUI.UpdateUI(this);
    }

    public int GetCellValue()
    {
        return currentValue;
    }

    public void TriggerCell()
    {
        List<(int, int)> actionCellShapeCoords = new List<(int, int)>();
        actionCellShapeCoords = CellShapeDB.shapes[CellActionShape].Invoke(this);
        List<Cell> potentialActionCells = GetCellsByRelativeCoords(actionCellShapeCoords);
        
        Action<Cell, Cell> action = CellActionDB.actions[cellActionType];

        // Move listener checks here
        foreach (Cell cell in potentialActionCells)
        {
            bool doAction = CellListenterDB.listeners[cell.CellActionListener].Invoke((this, cell));

            if (!doAction)
                continue;

            if (GameManager.Instance.CurrentStepActions.ContainsKey(cell)) {
                (Action<Cell, Cell>, Cell)  currentStoredAction = GameManager.Instance.CurrentStepActions[cell];

                if (currentStoredAction.Item2.actionPrecedence < actionPrecedence)
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
        List<Cell> triggerCells = GetCellsByRelativeCoords(triggerCellShapeCoords);
        triggerCellShapeCoords = CellShapeDB.shapes[cellTriggerShape].Invoke(this);
        foreach (Cell cell in triggerCells)
        {
            bool doTrigger = CellListenterDB.listeners[cell.CellTriggerListener].Invoke((this, cell));
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
