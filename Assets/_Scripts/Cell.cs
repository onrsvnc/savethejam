using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionPS;
    SoundManager soundManager;
    [SerializeField] private List<Sprite> cellSprites;
    public CellType currentCellType;
    public int row, col;
    public bool isFilled;

    void Awake() 
    {
        soundManager = FindObjectOfType<SoundManager>();
    }
    
    public void FillCell(CellType cellType)
    {
        isFilled = true;
        currentCellType = cellType;
        ChangeSprite();
    }

    public void CleanCell()
    {
        isFilled = false;
        currentCellType = CellType.Empty;
        ChangeSprite();
        ResetPos();
    }

    private void ResetPos()
    {
        transform.GetChild(0).transform.position = transform.position;
    }
    public void  UpgradeCell()
    {
        
        currentCellType++;
        ChangeSprite();
        int soundIndex = (int)currentCellType - 1;
        soundManager.PlayMergeSound(soundIndex);
        Instantiate<ParticleSystem>(explosionPS, transform);
        if (currentCellType == CellType.Trophy)
        {
           // Debug.Log("upgrade cell");
            GameManager.instance.trophyCount++;
            GameManager.instance.CheckWinState();
        }
        
        
        
        
    }

    private void ChangeSprite()
    {
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = cellSprites[(int)currentCellType];
    }
}
