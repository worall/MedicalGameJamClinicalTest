using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] CardBegin beginCard;

    private int m_patientImplication = 0;
    private int m_patientNumber = 0;
    private int m_scienceQuality = 0;
    private int m_money = 0;
    private int m_time = 0;

    private bool implicationComplete = false;
    private bool numberComplete = false;
    private bool scienceComplete = false;

    private int objectifCompleted = 0;

    private int m_currentTurn = 4;

    private int completion = 0;

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
        beginCard.onSwipeYes = StartGame;
        beginCard.onSwipeNo = StartGame;
    }

    private void StartGame(CardEffect effect)
    {
        UIManager.Instance.LauncheGamePanel();
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
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
            implicationComplete = true;
        else
            implicationComplete = false;

        if (m_patientNumber >= contract.patientNumberRequiremenent)
            numberComplete = true;
        else
            numberComplete = false;

        if (m_scienceQuality >= contract.scienceQualityRequirement)
            scienceComplete = true;
        else
            scienceComplete = false;

    }

    private void GoToNextTurn()
    {
        GenerateNewCard();

        if (m_time <= 0)
            EndGame();
    }

    private void EndGame()
    {
        if (implicationComplete)
            objectifCompleted++;

        if (numberComplete)
            objectifCompleted++;

        if (scienceComplete)
            objectifCompleted++;
    }

    private void GenerateNewCard()
    {
        CardContent cardContent= DeckManager.Instance.draw(m_currentTurn);
        m_currentTurn++;

        GameObject card = Instantiate(cardPrefab);
        card.SetActive(false);
        CardBehaviour cardBehaviour = card.GetComponent<CardBehaviour>();
        if (cardContent == null) {
            return;
        }
        cardBehaviour.cardContent = cardContent;
        cardBehaviour.cardEffectsYes = cardContent.yes;
        cardBehaviour.cardEffectsNo = cardContent.no;

        cardBehaviour.onSwipeYes = ApplyCardEffects;
        cardBehaviour.onSwipeNo = ApplyCardEffects;
        card.SetActive(true);
    }

    void ApplyCardEffects(CardEffect effects) {
        m_patientImplication += effects.implication;
        m_patientNumber += effects.patients;
        m_scienceQuality += effects.rigueur;
        m_money += effects.argent;

        StartCoroutine(DelayedCardPick());
    }

    IEnumerator DelayedCardPick() {
        yield return new WaitForSeconds(0.4f);
        this.GoToNextTurn();

        CheckStatisticStatue();
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
    }
}
