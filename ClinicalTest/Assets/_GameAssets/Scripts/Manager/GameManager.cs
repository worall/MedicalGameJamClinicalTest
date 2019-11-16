using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private int m_scienceQuality = 0;
    private int m_lifeQuality = 0;
    private int m_patientNumber = 0;
    private int m_patientVariety = 0;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OnStartGame += UIManager_OnStartGame;
        
    }

    private void UIManager_OnStartGame()
    {
        StartGame();
    }

    private void StartGame()
    {
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_lifeQuality, m_patientNumber, m_patientVariety);
    }
}
