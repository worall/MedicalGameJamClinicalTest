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

    private int m_currentTurn = 4;

    [SerializeField] public GameObject cardPrefab;

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
        GoToNextTurn();
    }

    private void UIManager_OnStartGame()
    {
        StartGame();
    }

    private void StartGame()
    {
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money);
        CheckStatisticStatue();
        GoToNextTurn();
    }

    private void OnContractSelected()
    {
        m_time = ContractManager.Instance.actualContract.timeToConclude;
    }

    private void CheckStatisticStatue()
    {
        Contract contract = ContractManager.Instance.actualContract;

        if (m_patientImplication >= contract.implicationRequirement)
            Debug.Log("science quality is well");

        if (m_patientNumber >= contract.patientNumberRequiremenent)
            Debug.Log("patient number is well");

        if (m_scienceQuality >= contract.scienceQualityRequirement)
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
        CardContent cardContent= DeckManager.Instance.draw(m_currentTurn);
        m_currentTurn++;

        GameObject card = Instantiate(cardPrefab);
        CardBehaviour cardBehaviour = card.GetComponent<CardBehaviour>();
        if (cardContent == null) {
            return;
        }
        cardBehaviour.cardIllustration = cardContent.image;
        cardBehaviour.cardTitle = cardContent.name;
        cardBehaviour.cardFlavorText = cardContent.situation;
        cardBehaviour.cardEffectsYes = cardContent.yes;
        cardBehaviour.cardEffectsNo = cardContent.no;

        cardBehaviour.onSwipeYes = ApplyCardEffects;
        cardBehaviour.onSwipeNo = ApplyCardEffects;
    }

    void ApplyCardEffects(CardEffect effects) {
        m_patientImplication += effects.implication;
        m_patientNumber += effects.patients;
        m_scienceQuality += effects.rigueur;
        m_money += effects.argent;
        this.GoToNextTurn();

        Debug.Log(m_patientImplication + "  implication");
        Debug.Log(m_patientNumber + "  number");
        Debug.Log(m_scienceQuality + "  rigueur");
        Debug.Log(m_money + "  money");

        CheckStatisticStatue();
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money);
    }
}
