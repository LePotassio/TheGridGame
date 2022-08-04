using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellCollider : MonoBehaviour
{
    [SerializeField]
    private Cell cell;
    private void OnMouseOver()
    {
        GameManager.Instance.SelectedCell = cell;

        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("Cell Clicked");
            GameManager.Instance.CellsToTriggerNow.Add(cell);
            GameManager.Instance.State = GameState.PuzzleStep;
            //cell.TriggerCell();
        }
    }
}
