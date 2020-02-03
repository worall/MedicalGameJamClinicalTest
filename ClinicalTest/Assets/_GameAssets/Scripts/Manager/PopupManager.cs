using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PopupManager : MonoBehaviour
{
    private static PopupManager _instance;
    public static PopupManager Instance { get { return _instance; } }

    [SerializeField] public OverlayCardContainerBehaviour overlayCardContainerPrefab;
    [SerializeField] public CardBehaviour popupCardPrefab;

    [SerializeField] GameObject tutoCard;

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

    }

    // Update is called once per frame
    void Update()
    {

    }

    // ALL AVAILABLE CONFIRM CARDS

    public void ShowQuitToHome() {
      OverlayCardContainerBehaviour overlay = this.InvokeOverlay();
      CardBehaviour card = this.InstantiateCard("quit");

      // Bind actions
      card.onSwipeYes = this.RestartGame;

      overlay.cardInstance = card;
    }

    public void ShowTryAgain() {
      OverlayCardContainerBehaviour overlay = this.InvokeOverlay();
      CardBehaviour card = this.InstantiateCard("try_again");

      card.onSwipeYes = this.TryAgain;

      overlay.cardInstance = card;
    }

    public void ShowConfirmRedirect() {
      OverlayCardContainerBehaviour overlay = this.InvokeOverlay();
      CardBehaviour card = this.InstantiateCard("redirect");

      card.onSwipeYes = this.Redirect;

      overlay.cardInstance = card;
    }

    public void ShowTuto() {
      OverlayCardContainerBehaviour overlay = this.InvokeOverlay();
      CardBehaviour card = this.InstantiateCard("tutoriel");

      // Create following tuto card
      GameObject tuto = Instantiate(this.tutoCard);
      tuto.SetActive(false);
      tuto.GetComponent<TutoCardBehaviour>().currentStep = 1;
      card.setFollowupCardYes(tuto);

      // Bind actions
      card.onSwipeYes = this.StartTutoriel;
      card.onSwipeNo = this.CancelTutoriel;

      overlay.cardInstance = card;
    }

    // UTILS

    OverlayCardContainerBehaviour InvokeOverlay() {
      //Debug.Log("Overlay is heeeere");
      OverlayCardContainerBehaviour overlay = Instantiate(overlayCardContainerPrefab);
      return overlay;
    }

    CardBehaviour InstantiateCard(string name) {
      CardBehaviour popupCard = Instantiate(popupCardPrefab);
      PopupCardContent content = this.GetContent(name);
      popupCard.GetComponent<PopupCardBehaviour>().popupContent = content;
      return popupCard;
    }

    PopupCardContent GetContent(string name) {
      PopupCardContent content = new PopupCardContent();

      switch(name) {
        case "tutoriel":
          content.title = "JOUER LE TUTORIEL ?";
          content.description = "Êtes-vous sûr de vouloir jouer à nouveau le tutoriel ?";
          content.swipeNoLabel = "Reprendre";
          content.swipeYesLabel = "Jouer à nouveau";
          break;

        case "quit":
          content.title = "QUITTER LA PARTIE ?";
          content.description = "Êtes-vous sûr de vouloir retourner à l'écran de titre ?";
          content.swipeNoLabel = "Reprendre";
          content.swipeYesLabel = "Quitter la partie";
          break;

        case "try_again":
          content.title = "REESSAYER ?";
          content.description = "Êtes-vous sûr de vouloir recommencer le contrat ?";
          content.swipeNoLabel = "Reprendre";
          content.swipeYesLabel = "Recommencer";
          break;
        
        case "redirect":
          content.title = "REDIRECTION";
          content.description = "Vous êtes sur le point d'être redirigé vers la page internet de Compare";
          content.swipeNoLabel = "Revenir";
          content.swipeYesLabel = "Ouvrir";
          break;
      }

      return content;
    }

    // CUSTOM HANDLERS

    bool RestartGame(CardEffect effects, bool swipedRight, GameObject followupCard) {
      GameManager.Instance.RestartGame();
      return true;
    }

    bool StartTutoriel(CardEffect effects, bool swipedRight, GameObject followupCard) {
      return true;
    }

    bool CancelTutoriel(CardEffect effects, bool swipedRight, GameObject followupCard) {
      followupCard = null;
      return true;
    }

    bool TryAgain(CardEffect effects, bool swipedRight, GameObject followupCard) {
      Debug.Log("Need to try again");
      GameManager.Instance.RetryContract();
      return true;
    }

    bool Redirect(CardEffect effects, bool swipedRight, GameObject followupCard) {
      Debug.Log("Need to redirect");
      Application.OpenURL("https://compare.aphp.fr/");
      return true;
    }
}
