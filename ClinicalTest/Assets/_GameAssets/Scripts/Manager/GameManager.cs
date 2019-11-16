using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private int m_patientImplication = 0;
    private int m_patientNumber = 0;
    private int m_scienceQuality = 0;
    private int m_money = 0;
    private int m_time = 0;

    private bool m_isImplicationNeeded = false;
    private bool m_isPatientNumberNeeded = false;
    private bool m_isScienceQualityNeeded = false;

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
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money);
        CheckStatisticStatue();
    }

    private void OnContractSelected()
    {
        m_time = ContractManager.Instance.actualContract.timeToConclude;
    }

    private void CheckStatisticStatue()
    {
        Contract contract = ContractManager.Instance.actualContract;

        if (m_isImplicationNeeded && m_patientImplication >= contract.implicationRequirement)
            Debug.Log("science quality is well");

        if (m_isPatientNumberNeeded && m_patientNumber >= contract.patientNumberRequiremenent)
            Debug.Log("patient number is well");

        if (m_isScienceQualityNeeded && m_scienceQuality >= contract.scienceQualityRequirement)
            Debug.Log("scienceQuality is well");
    }

    private void GoToNextTurn()
    {
        GenerateNewCard();
        if (m_time <= 0)
            Debug.Log("fin du jeu");

    }

    private void GenerateNewCard()
    {
        //CardStruct cardStruct = DeckManager.Instance.GetNewCard();
        //CardBehabiour card = Instantiate(cardPrefab);
        //card.Init(cardStruct);
    }
}
