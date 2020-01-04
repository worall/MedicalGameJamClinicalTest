using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Button homeButton;
    [SerializeField] public Button helpButton;

    [SerializeField] public OverlayCardContainerBehaviour overlayCardContainerPrefab;
    [SerializeField] public CardBehaviour helpCardPrefab;
    [SerializeField] public CardBehaviour popupCardPrefab;

    [SerializeField] public CardBehaviour jaugeInfoCardPrefab;
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
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClickHelp() {
        OverlayCardContainerBehaviour overlay = Instantiate(overlayCardContainerPrefab);
        overlay.cardInstance = Instantiate(helpCardPrefab);
    }

    void OnClickHome() {
        Debug.Log("Home clicked");
        OverlayCardContainerBehaviour overlay = Instantiate(overlayCardContainerPrefab);
        CardBehaviour popupCard = Instantiate(popupCardPrefab);
        PopupCardContent content = new PopupCardContent();
        content.title = "QUITTER LA PARTIE ?";
        content.description = "Êtes-vous sûr de vouloir retourner à l'écran de titre ?";
        content.options = "Glissez la carte :\n- à droite pour confirmer\n- à gauche pour reprendre";
        popupCard.GetComponent<PopupCardBehaviour>().popupContent = content;
        overlay.cardInstance = popupCard;
    }

    UnityAction OnClickJauge(JaugeType jaugeType) {
        return () => {
            OverlayCardContainerBehaviour overlay = Instantiate(overlayCardContainerPrefab);
            CardBehaviour jaugeCard = Instantiate(jaugeInfoCardPrefab);
            jaugeCard.GetComponent<JaugeInfoCardBehaviour>().jaugeType = jaugeType;
            overlay.cardInstance = jaugeCard;
        };
    }
}
