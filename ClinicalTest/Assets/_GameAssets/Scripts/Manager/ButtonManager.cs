using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Button homeButton;
    [SerializeField] public Button helpButton;

    [SerializeField] public OverlayCardContainerBehaviour overlayCardPrefab;
    [SerializeField] public CardBehaviour helpCard;
    [SerializeField] public CardBehaviour popupCard;

    [SerializeField] public PopupCardContent quitCardContent;

    [SerializeField] public JaugeInfoCardBehaviour jaugeInfoCardPrefab;
    [SerializeField] public Button implicationJaugeButton;
    [SerializeField] public Button patientsJaugeButton;
    [SerializeField] public Button rigueurJaugeButton;
    [SerializeField] public Button argentJaugeButton;

    // Start is called before the first frame update
    void Start()
    {
        homeButton.onClick.AddListener(OnClickHome);
        helpButton.onClick.AddListener(OnClickHelp);

        implicationJaugeButton.onClick.AddListener(OnClickJauge(JaugeType.IMPLICATION));
        patientsJaugeButton.onClick.AddListener(OnClickJauge(JaugeType.PATIENTS));
        rigueurJaugeButton.onClick.AddListener(OnClickJauge(JaugeType.RIGUEUR));
        argentJaugeButton.onClick.AddListener(OnClickJauge(JaugeType.ARGENT));

        PopupCardContent content = new PopupCardContent();
        content.title = "QUITTER LA PARTIE ?";
        content.description = "Êtes-vous sûr de vouloir retourner à l'écran de titre ?";
        content.options = "Glissez la carte :\n- à droite pour confirmer\n- à gauche pour reprendre";

        quitCardContent = content;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClickHelp() {
        OverlayCardContainerBehaviour overlay = Instantiate(overlayCardPrefab);
        overlay.cardPrefab = helpCard;
    }

    void OnClickHome() {
        Debug.Log("Home clicked");
        OverlayCardContainerBehaviour overlay = Instantiate(overlayCardPrefab);
        overlay.popupContent = quitCardContent;
        overlay.cardPrefab = popupCard;
    }

    UnityAction OnClickJauge(JaugeType jaugeType) {
        return () => {
            // TODO
            OverlayCardContainerBehaviour overlay = Instantiate(overlayCardPrefab);
            // JaugeInfoCardBehaviour jaugeCard = Instantiate(jaugeInfoCardPrefab);
            // jaugeCard.jaugeType = jaugeType;
            overlay.cardPrefab = jaugeInfoCardPrefab.GetComponent<CardBehaviour>();
        };
    }
}
