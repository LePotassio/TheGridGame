using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum GameState { PlayerTurn, PuzzleStepFirst, PuzzleStepInit, PuzzleStep }
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

    [SerializeField]
    private Cell playerCell;

    [SerializeField]
    private GameplayUI gameplayUI;


    public GameState State
    {
        get { return state; }
        set { state = value; gameplayUI.UpdateStateUI(); }
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

        gameplayUI.UpdateStateUI();
    }

    private void Update()
    {
        if (state == GameState.PlayerTurn)
        {
            // Wait for player to trigger a cell
        }
        else if (state == GameState.PuzzleStepInit)
        {
            // loop triggering until current step cells is empty

            // loop the dictionary and do all the actions
            State = GameState.PuzzleStep;
            StartCoroutine(DoPuzzleSteps());
        }
    }

    public void DoFirstAction(Cell triggeredCell)
    {
        State = GameState.PuzzleStepFirst;
        CellsToTriggerNow.Add(triggeredCell);
        StartCoroutine(DoFirstActionASync(triggeredCell));
    }

    public IEnumerator DoFirstActionASync(Cell triggeredCell)
    {
        Action<Cell, Cell> action = CellActionDB.actions[playerCell.CellData.CellActionType];
        switch (triggeredCell.CellData.CellType)
        {
            case CellType.DecrementDie:
                action.Invoke(playerCell, triggeredCell);
                break;
            default:
                break;
        }
        yield return null;

        State = GameState.PuzzleStepInit;
    }

    public IEnumerator DoPuzzleSteps()
    {
        yield return new WaitForSeconds(1f);
        // Wait here for all animations to finish

        currentStepActions = new Dictionary<Cell, (Action<Cell, Cell>, Cell)>();

        foreach (Cell cell in cellsToTriggerNow)
        {
            cell.TriggerCell();
        }

        foreach (var kvp in currentStepActions)
        {
            kvp.Value.Item1.Invoke(kvp.Value.Item2, kvp.Key);
        }

        cellsToTriggerNow = cellsToTriggerNext;

        if (cellsToTriggerNow.Count < 1)
            State = GameState.PlayerTurn;
        else
            State = GameState.PuzzleStepInit;

        cellsToTriggerNext = new List<Cell>();
    }
}
