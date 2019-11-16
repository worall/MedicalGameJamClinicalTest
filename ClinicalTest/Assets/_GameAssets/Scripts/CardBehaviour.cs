using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    [System.Serializable]
    public struct CardEffects
    {
        public float qualityModifier;
        public float quantityModifier;
        public string popupText;
    }

    public string cardTitle;
    public string cardFlavorText;
    public string cardIllustration;
    public CardEffects cardEffectsYes;
    public CardEffects cardEffectsNo;

    public delegate void OnSwipeDelegate(CardEffects effects);
    public OnSwipeDelegate onSwipeYes;
    public OnSwipeDelegate onSwipeNo;

    public bool swiped = false;
    public float vanishRatio = 0;
    const float VANISH_TIME = 0.6f;
    [SerializeField] public Image vanishOverlay;

    private Vector3? prevMousePos = null;
    private float width;
    private float height;

    void Awake()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        Text flavor = this.GetComponentsInChildren<Text>()[0];
        Text title = this.GetComponentsInChildren<Text>()[1];
        Image illustration = this.GetComponentsInChildren<Image>()[1];

        flavor.text = cardFlavorText;
        title.text = cardTitle;
        illustration.sprite = Resources.Load<Sprite>("illustrations/" + this.cardIllustration);
    }

    // Update is called once per frame
    void Update()
    {
        if (swiped) {
            vanishRatio = Mathf.Min(1, vanishRatio + Time.deltaTime / VANISH_TIME);
        }
        if (vanishRatio > 0) {
            Image[] images = GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++) {
                images[i].color = new Color(1, 1, 1, images[i].color.a * (1 - vanishRatio));
            }
            Text[] texts = GetComponentsInChildren<Text>();
            for (int i = 0; i < images.Length; i++) {
                texts[i].color = new Color(1, 1, 1, texts[i].color.a * (1 - vanishRatio));
            }
            vanishOverlay.color = new Color(1, 1, 1, vanishRatio < 0.5 ? vanishRatio * 2 : 4 - vanishRatio * 2);
        }
    }

    public void SwipeYes()
    {
        if (swiped) { return; }
        if (onSwipeYes != null)
        {
            this.onSwipeYes(cardEffectsYes);
            swiped = true;
            StartCoroutine(FinishSwipe());
        }
    }
    public void SwipeNo()
    {
        if (swiped) { return; }
        if (onSwipeNo != null)
        {
            this.onSwipeNo(cardEffectsNo);
            swiped = true;
            StartCoroutine(FinishSwipe());
        }
    }

    IEnumerator FinishSwipe()
    {
        yield return new WaitForSeconds(VANISH_TIME);
        GameObject.Destroy(gameObject);
    }

}
