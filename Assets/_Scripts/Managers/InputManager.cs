using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCam;
    [SerializeField] private InputTypeIndicator inputTypeIndicator;
    [SerializeField] private GridManager gridManager;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))  
        {
            if (GameManager.instance.currentGameState == GameState.Playing)
            {
                Cell hitCell = TryToGetHitCell();

                if (hitCell != null)
                {
                    if (!hitCell.isFilled)// cell is empty and ready to be filled
                    {
                        gridManager.isMerging = false;
                        hitCell.FillCell(inputTypeIndicator.currentInputCellType);
                        inputTypeIndicator.ChangeInput();
                        gridManager.CheckNeighbours(hitCell);
                        gridManager.CheckGrid();
                    }
                }
            }
            
        }
    }
    
    private Cell TryToGetHitCell()
    {
        RaycastHit2D hit = Physics2D.Raycast(mainCam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null && hit.collider.CompareTag("Cell"))
        {
            Cell cell = hit.collider.gameObject.GetComponent<Cell>();
            return cell;
        }
        
        return null; 
    }
}
