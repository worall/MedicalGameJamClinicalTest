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

    public event Action OnStartGame;

    // Start is called before the first frame update
    void Start()
    {
        mainPanel.OnButtonStartGame += MainPanel_OnButtonStartGame;
    }

    private void MainPanel_OnButtonStartGame()
    {
        OnStartGame?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
