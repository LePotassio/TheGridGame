using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { PlayerTurn, PuzzleStep }
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    [SerializeField]
    private CellGrid grid;

    private GameState state;


    private Dictionary<Cell, (Action<Cell, Cell>, Cell)> currentStepActions;

    private List<Cell> cellsToTriggerNow;

    private List<Cell> cellsToTriggerNext;

    private Cell selectedCell;

    public GameState State
    {
        get { return state; }
        set { state = value; }
    }

    public CellGrid Grid
    {
        get { return grid; }
    }

    public Dictionary<Cell, (Action<Cell, Cell>, Cell)> CurrentStepActions
    {
        get { return currentStepActions; }
    }

    public List<Cell> CellsToTriggerNow
    {
        get { return cellsToTriggerNow; }
    }

    public List<Cell> CellsToTriggerNext
    {
        get { return cellsToTriggerNext; }
    }

    public Cell SelectedCell
    {
        get { return selectedCell; }
        set { selectedCell = value; }
    }

    private void Awake()
    {
        Instance = this;
        selectedCell = null;

        cellsToTriggerNow = new List<Cell>();
        cellsToTriggerNext = new List<Cell>();
    }

    private void Start()
    {
        grid.InitializeGrid(10, 10);
    }

    private void Update()
    {
        if (state == GameState.PlayerTurn)
        {
            // Wait for player to trigger a cell
        }
        else if (state == GameState.PuzzleStep)
        {
            // loop triggering until current step cells is empty

            // loop the dictionary and do all the actions
            
            currentStepActions = new Dictionary<Cell, (Action<Cell, Cell>, Cell)>();

            foreach (Cell cell in cellsToTriggerNow)
            {
                cell.TriggerCell();
            }
            
            foreach(var kvp in currentStepActions)
            {
                kvp.Value.Item1.Invoke(kvp.Value.Item2, kvp.Key);
            }

            cellsToTriggerNow = cellsToTriggerNext;

            if (cellsToTriggerNow.Count < 1)
                state = GameState.PlayerTurn;

            cellsToTriggerNext = new List<Cell>();
        }
    }
}
