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

    // Start is called before the first frame update
    void Start()
    {
        homeButton.onClick.AddListener(OnClickHome);
        helpButton.onClick.AddListener(OnClickHelp);
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
    }
}
