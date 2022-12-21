using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridWidth, gridHeight;
    [SerializeField] private GridBuilder gridBuilder;
    [SerializeField] private float animationTime;
    private List<Cell> cellList = new List<Cell>();
    public bool isMerging;
    
    void Start()
    {
        gridBuilder.GenerateGrid(cellList, gridWidth, gridHeight);
    }
    
    public void CheckNeighbours(Cell cell)
    {
        List<Cell> mergeCellList = new List<Cell>();
        List<Cell> neighbourList = GetNeighbourCellList(cell);
        foreach (Cell neighbourCell in neighbourList)
        {
            if (neighbourCell.isFilled && neighbourCell.currentCellType == cell.currentCellType)
            {
                mergeCellList.Add(neighbourCell);
                List<Cell> secondaryNeighboursCellList = GetNeighbourCellList(neighbourCell);
                foreach (var secondaryNeighbourCell in secondaryNeighboursCellList)
                {
                    if (secondaryNeighbourCell.isFilled && secondaryNeighbourCell.currentCellType == cell.currentCellType)
                    {
                        if (!mergeCellList.Contains(secondaryNeighbourCell))
                        {
                            mergeCellList.Add(secondaryNeighbourCell);
                        }
                    }
                }
            }
        }
        
        if (mergeCellList.Count>=3)
        {
            Merge(mergeCellList,cell);
            isMerging = true;
        }
        
    }

    public void CheckGrid()
    {
        int count = 0;
        foreach (Cell cell in cellList)
        {
            if (cell.isFilled)
            {
                count++;
            }
        }
        
        if (cellList.Count == count)
        {
            if (!isMerging)
            {
                GameManager.instance.CheckLoseState();
            }
        }
    }

    private void Merge(List<Cell> cellList, Cell targetCell)
    {
        foreach (Cell cellToMerge in cellList)
        {
            if (cellToMerge == targetCell)
            {
                targetCell.UpgradeCell();
                targetCell.transform.DOMove(targetCell.transform.position, animationTime)
                    .OnComplete(() => CheckNeighbours(targetCell));
            }
            else
            {
                cellToMerge.transform.GetChild(0).transform.DOMove(targetCell.transform.position, animationTime).OnComplete(()=>cellToMerge.CleanCell());
            }
        }
    }
    
    private List<Cell> GetNeighbourCellList(Cell cell)
    {
        List<Cell> neighbourList = new List<Cell>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (Mathf.Abs(i) != Mathf.Abs(j))
                {
                    if (TryToGetCell(cell.row + i, cell.col + j) != null)
                    {
                        neighbourList.Add(TryToGetCell(cell.row + i, cell.col + j));
                    }
                }
            }
        }
        
        return neighbourList;
    }
    

    private Cell TryToGetCell(int row, int col)
    {
        if (row > gridWidth - 1 || row < 0 || col > gridHeight - 1 || col < 0)
        {
            return null;
        }


        return cellList[row * gridWidth + col];
    }
}
    
