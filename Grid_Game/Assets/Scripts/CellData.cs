using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CellActionType { AddMagnitude }
public enum CellTriggerShape { Square, Cross, List }
public enum CellListener { MatchMagnitude }

[System.Serializable]
public class CellData
{
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
    private int minValue;

    [SerializeField]
    private int maxValue;

    [SerializeField]
    private int magnitude = -1;

    [SerializeField]
    private int listenerMagnitude = 0;

    [SerializeField]
    private int actionPrecedence = 0;

    [SerializeField]
    private bool playerTriggerable = true;

    [SerializeField]
    private CellType cellType;

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

    public int MinValue
    {
        get { return minValue; }
        set { minValue = value; }
    }

    public int MaxValue
    {
        get { return maxValue; }
        set { maxValue = value; }
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

    public int ActionPrecedence
    {
        get { return actionPrecedence; }
        set { actionPrecedence = value; }
    }

    public bool PlayerTriggerable
    {
        get { return playerTriggerable; }
        set { playerTriggerable = value; }
    }

    public CellType CellType
    {
        get { return cellType; }
        set { cellType = value; }
    }
}
