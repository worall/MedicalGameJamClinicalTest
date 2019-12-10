using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] public Button homeButton;
    [SerializeField] public Button helpButton;

    [SerializeField] public OverlayCardContainerBehaviour overlayCardPrefab;
    [SerializeField] public CardBehaviour helpCard;
    [SerializeField] public CardBehaviour popupCard;

    [SerializeField] public PopupCardContent quitCardContent;

    // Start is called before the first frame update
    void Start()
    {
        homeButton.onClick.AddListener(OnClickHome);
        helpButton.onClick.AddListener(OnClickHelp);

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
}
