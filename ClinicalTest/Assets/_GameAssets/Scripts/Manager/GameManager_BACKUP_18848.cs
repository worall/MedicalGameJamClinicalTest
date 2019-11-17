using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    [SerializeField] GameObject beginCard;
    [SerializeField] GameObject compareCard;
    [SerializeField] GameObject compareLinkCard;
    [SerializeField] GameObject creditCard;
    [SerializeField] EndCardBehaviour endCardPrefab;

    private EndCardBehaviour m_endCard;
<<<<<<< HEAD

    private int m_patientImplication = 50;
    private int m_patientNumber = 50;
    private int m_scienceQuality = 50;
    private int m_money = 50;
    private int m_time = 50;
=======
    
    private int m_patientImplication = 0;
    private int m_patientNumber = 0;
    private int m_scienceQuality = 0;
    private int m_money = 0;
    private int m_time = 0;
>>>>>>> e717635f2ce67e5db331371658d2133f2ee7c02f

    private bool implicationComplete = false;
    private bool numberComplete = false;
    private bool scienceComplete = false;

    private int objectifCompleted = 0;

    private int m_currentTurn = 1;

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
        GameObject beginCardInst = Instantiate(beginCard);
        CardBehaviour behaviour = beginCardInst.GetComponent<CardBehaviour>();
        behaviour.onSwipeYes = FirstEffectSwipe;
        behaviour.onSwipeNo = FirstEffectSwipe;
    }

    private void StartGame()
    {
        UIManager.Instance.LauncheGamePanel();
        UIManager.Instance.gamePanel.Init(50);
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
        CheckStatisticStatue();
        GoToNextTurn();
        // OpenCompareCard();
        // OpenCreditCard();
    }

    private void OnContractSelected()
    {
        m_time = ContractManager.Instance.actualContract.timeToConclude;
    }

    private void CheckStatisticStatue()
    {
      return;
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

        //if (m_time <= 0)
        //    EndGame();
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

        m_patientImplication = Mathf.Clamp(m_patientImplication + effects.implication, 0, 100);
        m_patientNumber = Mathf.Clamp(m_patientNumber + effects.patients, 0, 100);
        m_scienceQuality = Mathf.Clamp(m_scienceQuality + effects.rigueur, 0, 100);
        m_money = Mathf.Clamp(m_money + effects.argent, 0, 100);

        Debug.Log(m_money + "   " + effects.argent);

        StartCoroutine(DelayedCardPick());
    }

    void FirstEffectSwipe(CardEffect effect) {
        StartCoroutine(DelayedStartGame());
    }

    IEnumerator DelayedStartGame() {
        yield return new WaitForSeconds(0.4f);
        this.StartGame();
    }

    IEnumerator DelayedCardPick() {
        yield return new WaitForSeconds(0.4f);
        this.GoToNextTurn();

        CheckStatisticStatue();
        UIManager.Instance.gamePanel.UpdateStats(m_scienceQuality, m_patientImplication, m_patientNumber, m_money, m_time);
    }

    // COMPARE CARD

    private void OpenCompareCard() {
        GameObject compareCardInst = Instantiate(compareCard);
        CardBehaviour behaviour = compareCardInst.GetComponent<CardBehaviour>();
        behaviour.onSwipeYes = OpenSecondCompareCard;
        behaviour.onSwipeNo = OpenSecondCompareCard;
    }

    private void OpenSecondCompareCard(CardEffect effect) {
        GameObject compareLinkCardInst = Instantiate(compareLinkCard);
        CardBehaviour behaviour = compareLinkCardInst.GetComponent<CardBehaviour>();
        behaviour.onSwipeYes = OpenCompareURL;
        behaviour.onSwipeNo = OnFinishCompare;
    }

    private void OpenCompareURL(CardEffect effect) {
      Debug.Log("Opening URL...");
      Application.OpenURL("https://www.google.com");
    }

    private void OnFinishCompare(CardEffect effect) {
      Debug.Log("Restarting game...");
    }

    // CREDIT CARD

    private void OpenCreditCard() {
        GameObject creditCardInst = Instantiate(creditCard);
        CardBehaviour behaviour = creditCardInst.GetComponent<CardBehaviour>();
        behaviour.onSwipeYes = SwipeAwayFromCredit;
        behaviour.onSwipeNo = SwipeAwayFromCredit;
    }

    private void SwipeAwayFromCredit(CardEffect effect) {
      Debug.Log("Opening URL...");
      // Call whatever you like
    }
}
