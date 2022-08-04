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

        if (Input.GetMouseButtonDown(0) && GameManager.Instance.State == GameState.PlayerTurn)
        {
            // Debug.Log("Cell Clicked");
            if (cell.CellData.PlayerTriggerable)
            {
                GameManager.Instance.DoFirstAction(cell);
                //cell.TriggerCell();
            }
        }
    }
}
