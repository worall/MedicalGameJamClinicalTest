using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum STEPS {
    INTRO,
    TUTO,
    CONTRAT,
    GAME,
    GAME_FEEDBACK,
    GAME_ENDED,
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
    [SerializeField] GameObject feedbackCard;
    [SerializeField] GameObject contratCard;

    private EndCardBehaviour m_endCard;

    private int m_patientImplication = 50;
    private int m_patientNumber = 50;
    private int m_scienceQuality = 50;
    private int m_money = 50;
    private int m_time = 50;

    private bool implicationComplete = false;
    private bool numberComplete = false;
    private bool scienceComplete = false;

    private int objectifCompleted = 0;

    private int m_currentTurn = 1;

    private int completion = 0;

    [SerializeField] public GameObject cardPrefab;

    private STEPS currentStep;
    private int currentContract;

    private int m_tutoStep = 0;

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
        DeckManager.Instance.initPools();
        UIManager.Instance.LauncheGamePanel();
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
        CheckStatisticStatue();

        GenerateNewGameCard();
    }

    private void OnContractSelected()
    {
        Contract contract = ContractManager.Instance.GetCurrentContract();
        m_time = contract.timeToConclude;
        m_patientImplication = contract.implactionBaseValue;
        m_patientNumber = contract.numberPatientBaseValue;
        m_scienceQuality = contract.scienceQualityBaseValue;
        m_money = contract.moneyBaseValue;
        m_currentTurn = 1;
        objectifCompleted = 0;
    }

    private void CheckStatisticStatue()
    {
        Contract contract = ContractManager.Instance.GetCurrentContract();

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
        CardBehaviour card = m_endCard.GetComponent<CardBehaviour>();
        card.onSwipeYes = HandleCardSwipe;
        card.onSwipeNo = HandleCardSwipe;
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
                cardInst = Instantiate(contratCard);
                cardInst.GetComponent<ContractCardBehaviour>().currentContract =
                    ContractManager.Instance.GetCurrentContract();
                behaviour = cardInst.GetComponent<CardBehaviour>();
                behaviour.onSwipeYes = HandleCardSwipe;
                behaviour.onSwipeNo = HandleCardSwipe;
                break;
            case STEPS.GAME:
                // should not happen
                break;
            case STEPS.GAME_ENDED:
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

        cardBehaviour.onSwipeYes = HandleCardSwipe;
        cardBehaviour.onSwipeNo = HandleCardSwipe;
        card.SetActive(true);
    }

    private void GenerateNewFeedbackCard(CardEffect effect)
    {
        GameObject card = Instantiate(feedbackCard);
        FeedbackBehaviour feedbackBehaviour = card.GetComponent<FeedbackBehaviour>();
        feedbackBehaviour.effect = effect;

        CardBehaviour cardBehaviour = card.GetComponent<CardBehaviour>();
        cardBehaviour.onSwipeYes = HandleCardSwipe;
        cardBehaviour.onSwipeNo = HandleCardSwipe;
    }

    bool HandleCardSwipe(CardEffect effect, bool swipedRight, GameObject followupCard) {
        UIManager.Instance.ClearEffectPreview();

        if (currentStep == STEPS.GAME) {
            if (m_money + effect.argent < 0) {
                Camera.main.GetComponent<CameraShakeBehaviour>().Shake();
                UIManager.Instance.FlashMoneyIcon();
                return false;
            }

            m_patientImplication = Mathf.Clamp(m_patientImplication + effect.implication, 0, 100);
            m_patientNumber = Mathf.Clamp(m_patientNumber + effect.patients, 0, 100);
            m_scienceQuality = Mathf.Clamp(m_scienceQuality + effect.rigueur, 0, 100);
            m_money = Mathf.Clamp(m_money + effect.argent, 0, 100);
            m_time = Mathf.Clamp(m_time - effect.cost, 0, 100);
            UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
        }

        if (currentStep == STEPS.COMPARE_LINK && swipedRight) {
            PopupManager.Instance.ShowConfirmRedirect();
            return false;
        }

        if (currentStep == STEPS.STARS && !swipedRight) {
            PopupManager.Instance.ShowTryAgain();
            return false;
        }

        StartCoroutine(CardSwipeCoroutine(effect, swipedRight, followupCard));

        return true;
    }

    IEnumerator CardSwipeCoroutine(CardEffect effects, bool swipedRight, GameObject followupCard) {
        yield return new WaitForSeconds(0.4f);

        if (followupCard != null) {
            CardBehaviour cardBehaviour = followupCard.GetComponent<CardBehaviour>();
            cardBehaviour.onSwipeYes = HandleCardSwipe;
            cardBehaviour.onSwipeNo = HandleCardSwipe;
            followupCard.SetActive(true);
            yield break;
        }

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
                OnContractSelected();
                this.StartGame();
                break;
            case STEPS.GAME:
                if (effects.debrief == "") {
                    this.GenerateNewGameCard();
                    CheckStatisticStatue();
                    currentStep = STEPS.GAME;
                } else {
                    this.GenerateNewFeedbackCard(effects);
                    currentStep = STEPS.GAME_FEEDBACK;
                }
                if (m_time <= 0) {
                    currentStep = STEPS.GAME_ENDED;
                }
                break;
            case STEPS.GAME_FEEDBACK:
                this.GenerateNewGameCard();
                CheckStatisticStatue();
                currentStep = STEPS.GAME;
                break;
            case STEPS.GAME_ENDED:
                currentStep = STEPS.STARS;
                GenerateNewUniqueCard();
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

    public void RestartGame() {
        //TODO: Actually restart the game
        Debug.Log("restarted");
    }

    public void RetryContract() {
      Debug.Log("Restarting contract...");
    
      // TODO: Remove the card at the back...

      currentStep = STEPS.CONTRAT;
      GenerateNewUniqueCard();
    }
}
