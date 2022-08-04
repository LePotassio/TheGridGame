using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellGrid : MonoBehaviour
{
    private Cell[,] grid;

    public void InitializeGrid(int numRows, int numCols)
    {
        grid = new Cell[numRows, numCols];

        GameObject cellPrefab = Resources.Load("Cells/Cell") as GameObject;

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                // Initialize a cell game object
                GameObject newCellObj = Instantiate(cellPrefab, transform);
                // Adjust
                newCellObj.transform.position = new Vector3(row, col, 0);
                // Assign it's cell component to the grid
                Cell newCell = newCellObj.GetComponent<Cell>();
                newCell.RowPosition = row;
                newCell.ColumnPosition = col;

                // Temp
                CellFactory.SetCell(newCell, 6, CellType.DecrementDie);

                grid[row, col] = newCell;
            }
        }
    }

    public void SetGridCell(int row, int col, int val)
    {
        grid[row, col].SetCellValue(val);
    }

    public Cell GetGridCell(int row, int col)
    {
        if (row >= 0 && col >= 0 && row < grid.GetLength(0) && col < grid.GetLength(1))
            return grid[row, col];
        return null;
    }
}
