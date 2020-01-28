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
        Debug.Log("Help clicked");
        PopupManager.Instance.ShowTuto();
    }

    void OnClickHome() {
        Debug.Log("Home clicked");
        PopupManager.Instance.ShowQuitToHome();
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
