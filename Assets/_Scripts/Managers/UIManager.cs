using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Image endPanel;
    [SerializeField] private Text endText;
    void Start()
    {
        endPanel.gameObject.SetActive(false);

    }
    

    private void ActivateEndPanel(GameState gameState)
    {
        if (gameState == GameState.Win)
        {
            endPanel.gameObject.SetActive(true);
            endText.text = "VICTORY";

        }
        if (gameState == GameState.Lose)
        {
            endPanel.gameObject.SetActive(true);
            endText.text = "DEFEAT";
        }
    }
    
    public void RestartButton()
    {
        SceneManager.LoadScene(0);
    }
    private void OnEnable()
    {
        GameManager.instance.OnGameStateChanged += ActivateEndPanel;
    }

    private void OnDisable()
    {
        GameManager.instance.OnGameStateChanged -= ActivateEndPanel;

    }
}
