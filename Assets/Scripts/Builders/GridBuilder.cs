using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBuilder : MonoBehaviour
{
    [SerializeField] private SpriteRenderer gridBackground;
    [SerializeField] private Cell cellPrefab;
    [SerializeField] private List<Sprite> cellBackgroundSprites;

    public void GenerateGrid(List<Cell> cellList,int gridSizeX, int gridSizeY)
    {
        float gridWidth = gridBackground.bounds.size.x * gridBackground.transform.localScale.x;
        float gridHeight = gridBackground.bounds.size.y * gridBackground.transform.localScale.y;
        
        float cellScaleX = gridWidth / gridSizeX;
        float cellScaleY = gridHeight / gridSizeY;

        float defaultPosX = gridBackground.transform.position.x - gridWidth / 2 + cellScaleX/2;
        float defaultPosY = gridBackground.transform.position.y + gridHeight / 2 - cellScaleY/2;

        for (int row = 0; row < gridSizeX; row++)
        {
            for (int col = 0; col < gridSizeY; col++)
            {
                float newCellPosX = defaultPosX + cellScaleX * col;
                float newCellPosY = defaultPosY - cellScaleY * row;
                Cell createdCell = GetCreatedCell(new Vector2(newCellPosX, newCellPosY),
                    new Vector2(cellScaleX, cellScaleY), row, col);
                cellList.Add(createdCell);
                int value = (row + col) % 2;
                createdCell.GetComponent<SpriteRenderer>().sprite = cellBackgroundSprites[value];

            }
        }
    }
    
    private Cell GetCreatedCell(Vector2 cellPosition,Vector2 cellScale,int row , int col)
    {
        Cell createdCell = Instantiate(cellPrefab);
        createdCell.transform.position = cellPosition;
        createdCell.transform.localScale = cellScale;
        createdCell.transform.SetParent(gridBackground.transform);
        createdCell.name = "Cell " + row + "X" + col;
        createdCell.row = row;
        createdCell.col = col;
        return createdCell;
    }
}
