using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    public string cardTitle;
    public string cardFlavorText;
    public string cardIllustration;
    public CardEffect cardEffectsYes;
    public CardEffect cardEffectsNo;

    public delegate void OnSwipeDelegate(CardEffect effects);
    public OnSwipeDelegate onSwipeYes;
    public OnSwipeDelegate onSwipeNo;

    public bool swiped = false;
    public float vanishRatio = 0;
    const float VANISH_TIME = 0.28f;
    [SerializeField] public Image vanishOverlay;

    private Vector3? prevMousePos = null;

    void Awake()
    {
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
            float alpha = Mathf.Min(1, 2 - vanishRatio * 2);
            Image[] images = GetComponentsInChildren<Image>();
            for (int i = 0; i < images.Length; i++) {
                images[i].color = new Color(1, 1, 1, images[i].color.a * alpha);
            }
            Text[] texts = GetComponentsInChildren<Text>();
            for (int i = 0; i < texts.Length; i++) {
                texts[i].color = new Color(1, 1, 1, texts[i].color.a * alpha);
            }
            vanishOverlay.color = new Color(1, 1, 1, Mathf.Min(1, vanishRatio) * alpha);

            float s = 1 + 0.03f * vanishRatio;
            transform.localScale = new Vector3(s, s, s);
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
