using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
