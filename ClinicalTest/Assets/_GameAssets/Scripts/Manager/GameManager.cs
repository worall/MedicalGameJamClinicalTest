using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum STEPS {
    INTRO,
    TUTO,
    CONTRAT,
    GAME,
    STARS,
    COMPARE,
    COMPARE_LINK,
    CREDITS
}

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] GameObject beginCard;
    [SerializeField] GameObject compareCard;
    [SerializeField] GameObject compareLinkCard;
    [SerializeField] GameObject creditCard;
    [SerializeField] EndCardBehaviour endCardPrefab;
    [SerializeField] GameObject tutoCard;
    [SerializeField] GameObject creditsCard;
    [SerializeField] GameObject[] contratCards;

    private EndCardBehaviour m_endCard;

    private int m_patientImplication = 0;
    private int m_patientNumber = 0;
    private int m_scienceQuality = 0;
    private int m_money = 0;
    private int m_time = 0;

    private bool implicationComplete = false;
    private bool numberComplete = false;
    private bool scienceComplete = false;

    private int objectifCompleted = 0;

    private int m_currentTurn = 1;

    private int completion = 0;

    [SerializeField] public GameObject cardPrefab;

    private STEPS currentStep;
    private int currentContract;

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
        GameObject beginCardInst = Instantiate(beginCard);
        CardBehaviour behaviour = beginCardInst.GetComponent<CardBehaviour>();
        behaviour.onSwipeYes = HandleCardSwipe;
        behaviour.onSwipeNo = HandleCardSwipe;
    }

    private void StartGame()
    {
        UIManager.Instance.LauncheGamePanel();
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
        CheckStatisticStatue();

        GenerateNewGameCard();
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

    private void EndGame()
    {
        if (implicationComplete)
            objectifCompleted++;

        if (numberComplete)
            objectifCompleted++;

        if (scienceComplete)
            objectifCompleted++;

        m_endCard = Instantiate(endCardPrefab);
        m_endCard.validStars = objectifCompleted;
    }

    private void GenerateNewUniqueCard()
    {
        GameObject cardInst;
        CardBehaviour behaviour;

        switch(currentStep) {
            case STEPS.INTRO:
                // should not happen
                break;
            case STEPS.TUTO:
                cardInst = Instantiate(tutoCard);
                behaviour = cardInst.GetComponent<CardBehaviour>();
                behaviour.onSwipeYes = HandleCardSwipe;
                behaviour.onSwipeNo = HandleCardSwipe;
                break;
            case STEPS.CONTRAT:
                cardInst = Instantiate(contratCards[currentContract]);
                behaviour = cardInst.GetComponent<CardBehaviour>();
                behaviour.onSwipeYes = HandleCardSwipe;
                behaviour.onSwipeNo = HandleCardSwipe;
                break;
            case STEPS.GAME:
                // should not happen
                break;
            case STEPS.STARS:
                EndGame();
                break;
            case STEPS.COMPARE:
                cardInst = Instantiate(compareCard);
                behaviour = cardInst.GetComponent<CardBehaviour>();
                behaviour.onSwipeYes = HandleCardSwipe;
                behaviour.onSwipeNo = HandleCardSwipe;
                break;
            case STEPS.COMPARE_LINK:
                cardInst = Instantiate(compareLinkCard);
                behaviour = cardInst.GetComponent<CardBehaviour>();
                behaviour.onSwipeYes = HandleCardSwipe;
                behaviour.onSwipeNo = HandleCardSwipe;
                break;
            case STEPS.CREDITS:
                cardInst = Instantiate(creditsCard);
                behaviour = cardInst.GetComponent<CardBehaviour>();
                behaviour.onSwipeYes = HandleCardSwipe;
                behaviour.onSwipeNo = HandleCardSwipe;
                break;
        }
    }

    private void GenerateNewGameCard()
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

        cardBehaviour.onSwipeYes = HandleCardSwipe;
        cardBehaviour.onSwipeNo = HandleCardSwipe;
        card.SetActive(true);
    }


    void HandleCardSwipe(CardEffect effect, bool swipedRight) {
        if (currentStep == STEPS.GAME) {
            m_patientImplication += effect.implication;
            m_patientNumber += effect.patients;
            m_scienceQuality += effect.rigueur;
            m_money += effect.argent;
            m_time -= effect.cost;

            if (m_time <= 0) {
                currentStep = STEPS.STARS;
            }
        }

        StartCoroutine(CardSwipeCoroutine(swipedRight));
    }

    IEnumerator CardSwipeCoroutine(bool swipedRight) {
        yield return new WaitForSeconds(0.4f);
        switch(currentStep) {
            case STEPS.INTRO:
                currentStep = STEPS.TUTO;
                GenerateNewUniqueCard();
                break;
            case STEPS.TUTO:
                currentStep = STEPS.CONTRAT;
                GenerateNewUniqueCard();
                break;
            case STEPS.CONTRAT:
                currentStep = STEPS.GAME;
                ContractManager.Instance.SelectNextContrat();
                OnContractSelected();
                this.StartGame();
                break;
            case STEPS.GAME:
                this.GenerateNewGameCard();
                CheckStatisticStatue();
                UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
                break;
            case STEPS.STARS:
                currentStep = STEPS.COMPARE;
                GenerateNewUniqueCard();
                break;
            case STEPS.COMPARE:
                currentStep = STEPS.COMPARE_LINK;
                GenerateNewUniqueCard();
                break;
            case STEPS.COMPARE_LINK:
                currentContract++;
                ContractManager.Instance.OnContractEnded();
                if (currentContract > 2) {
                    if (swipedRight) {
                        Debug.Log("Opening URL...");
                        Application.OpenURL("https://www.google.com");
                    }
                    currentStep = STEPS.CREDITS;
                    GenerateNewUniqueCard();
                } else {
                    Debug.Log("Restarting game...");
                    currentStep = STEPS.CONTRAT;
                    GenerateNewUniqueCard();
                }
                break;
            case STEPS.CREDITS:
                // Call whatever you like
                break;
        }
    }

}
