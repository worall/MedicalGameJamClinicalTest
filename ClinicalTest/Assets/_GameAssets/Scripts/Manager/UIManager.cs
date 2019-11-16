using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public MainPanel mainPanel;
    public GamePanel gamePanel;
    public EndPanel endPanel;

    public event Action OnStartGame;
    private Panel actualPanel;

    // Start is called before the first frame update
    void Start()
    {
        actualPanel = mainPanel;
        mainPanel.OnButtonStartGame += MainPanel_OnButtonStartGame;
    }

    public void LauncheGamePanel()
    {
        AddPanel(gamePanel);
    }

    private void MainPanel_OnButtonStartGame()
    {
        OnStartGame?.Invoke();
        AddPanel(gamePanel);
    }

    private void AddPanel(Panel panel)
    {
        actualPanel.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
        actualPanel = panel;
    }
}
