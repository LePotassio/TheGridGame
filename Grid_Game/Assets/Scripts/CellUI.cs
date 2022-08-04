using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellUI : MonoBehaviour
{
    [SerializeField]
    private TextMesh textMesh;

    public void UpdateUI(Cell c)
    {
        textMesh.text = "" + c.GetCellValue();
    }
}
