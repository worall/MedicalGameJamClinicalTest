using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    public bool floating = false;

    [SerializeField] public CardContent cardContent;
    public CardEffect cardEffectsYes;
    public CardEffect cardEffectsNo;

    public bool forceEffectShow = false;

    // If returns false, then swipe is prevented (ie not enough money)
    public delegate bool OnSwipeDelegate(CardEffect effects, bool swipedRight);
    public OnSwipeDelegate onSwipeYes;
    public OnSwipeDelegate onSwipeNo;

    bool swiped = false;
    float vanishRatio = 0;
    protected float VANISH_TIME = 0.34f;

    [SerializeField] public CanvasGroup cardMainCanvas;
    [SerializeField] public CanvasGroup cardEffectCanvas;
    [SerializeField] public Text choiceText;
    [SerializeField] public Text weekCostText;
    [SerializeField] public Transform relativePos;

    private Vector3? prevMousePos = null;

    private float initialEffectAlpha;

    const float DISTANCE_EFFECT_SHOW = 50;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Text flavor = this.GetComponentsInChildren<Text>()[0];
        Text title = this.GetComponentsInChildren<Text>()[1];
        Image illustration = this.GetComponentsInChildren<Image>()[1];

        if (cardContent != null) {
            flavor.text = cardContent.situation;
            title.text = cardContent.name.ToUpper();
            Sprite sprite = Resources.Load<Sprite>("illustrations/" + this.cardContent.image);
            if (sprite != null) {
                illustration.sprite = sprite;
            }
        }

        // temp: hide effect canvas if no card content is given
        if (cardContent == null && !forceEffectShow) {
            cardEffectCanvas.gameObject.SetActive(false);
        } else {
            cardEffectCanvas.gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (cardContent != null) {
            if (relativePos.localPosition.x > 0) {
                choiceText.text = cardContent.yes.choice.ToUpper();
                weekCostText.text = '-' + cardContent.yes.cost.ToString();
            } else {
                choiceText.text = cardContent.no.choice.ToUpper();
                weekCostText.text = '-' + cardContent.no.cost.ToString();
            }
        }

        if (swiped) {
            vanishRatio = Mathf.Min(1, vanishRatio + Time.deltaTime / VANISH_TIME);
        }
        if (vanishRatio > 0) {
            if (initialEffectAlpha == 0 && cardEffectCanvas) {
                initialEffectAlpha = cardEffectCanvas.alpha;
            }
            float alpha = 1 - vanishRatio;
            Vector3 offset = new Vector3(relativePos.localPosition.x, relativePos.localPosition.y, relativePos.localPosition.z);
            offset.Normalize();
            relativePos.localPosition += offset * 120f * (1 - 0.3f * vanishRatio);

            //float s = 1 + 0.03f * vanishRatio;
            //transform.localScale = new Vector3(s, s, s);
        }

        if (forceEffectShow) {
            cardEffectCanvas.alpha = 1;
        } else if (!swiped && cardEffectCanvas != null) {
            cardEffectCanvas.alpha = Mathf.Abs(relativePos.localPosition.x) / DISTANCE_EFFECT_SHOW;
        }
    }

    public void SwipeYes()
    {
        if (swiped) { return; }
        swiped = true;
        if (onSwipeYes != null) {
            swiped = this.onSwipeYes(cardEffectsYes, true);
        }
        if (swiped) {
            StartCoroutine(FinishSwipe());
        }
    }
    public void SwipeNo()
    {
        if (swiped) { return; }
        swiped = true;
        if (onSwipeNo != null) {
            swiped = this.onSwipeNo(cardEffectsNo, true);
        }
        if (swiped) {
            StartCoroutine(FinishSwipe());
        }
    }

    IEnumerator FinishSwipe()
    {
        yield return new WaitForSeconds(VANISH_TIME);
        GameObject.Destroy(gameObject);
    }

    public bool isSwiped() {
        return swiped;
    }

}
