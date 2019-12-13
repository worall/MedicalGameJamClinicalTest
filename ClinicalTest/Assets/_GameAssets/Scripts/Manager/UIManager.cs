using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    public MainPanel mainPanel;
    public GamePanel gamePanel;
    public EndPanel endPanel;

    public event Action OnStartGame;
    private Panel actualPanel;

    [SerializeField] public Text weekCostText;
    [SerializeField] public Image implicationEffectImage;
    [SerializeField] public Image rigueurEffectImage;
    [SerializeField] public Image patientsEffectImage;
    [SerializeField] public Image argentEffectImage;

    // Start is called before the first frame update
    void Start()
    {
        actualPanel = mainPanel;
        mainPanel.OnButtonStartGame += MainPanel_OnButtonStartGame;
        ClearEffectPreview();
    }

    public void LauncheGamePanel()
    {
        AddPanel(gamePanel);
    }

    private void MainPanel_OnButtonStartGame()
    {
        OnStartGame?.Invoke();
        AddPanel(gamePanel);
    }

    private void AddPanel(Panel panel)
    {
        actualPanel.gameObject.SetActive(false);
        panel.gameObject.SetActive(true);
        actualPanel = panel;
    }

    public void ClearEffectPreview() {
        PreviewEffect(new CardEffect(), 0);
    }

    public void PreviewEffect(CardEffect effect, float opacity) {
        weekCostText.text = effect.cost != 0 ? (-effect.cost).ToString() : "";
        weekCostText.color = new Color(weekCostText.color.r, weekCostText.color.g, weekCostText.color.b, effect.cost != 0 ? opacity : 1);
        implicationEffectImage.color = new Color(1, 1, 1, effect.implication != 0 ? opacity : 0);
        rigueurEffectImage.color = new Color(1, 1, 1, effect.rigueur != 0 ? opacity : 0);
        patientsEffectImage.color = new Color(1, 1, 1, effect.patients != 0 ? opacity : 0);
        argentEffectImage.color = new Color(1, 1, 1, effect.argent != 0 ? opacity : 0);
    }
}
