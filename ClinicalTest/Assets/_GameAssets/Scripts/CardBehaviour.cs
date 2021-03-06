﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBehaviour : MonoBehaviour
{
    public bool floating = false;

    [SerializeField] public CardContent cardContent;

    public bool forceEffectShow = false;

    // If returns false, then swipe is prevented (ie not enough money)
    public delegate bool OnSwipeDelegate(CardEffect effects, bool swipedRight, GameObject followupCard);
    public OnSwipeDelegate onSwipeYes;
    public OnSwipeDelegate onSwipeNo;

    bool swiped = false;
    float vanishRatio = 0;
    protected float VANISH_TIME = 0.34f;

    [SerializeField] public CanvasGroup cardMainCanvas;
    [SerializeField] public CanvasGroup cardEffectCanvas;

    [SerializeField] public Text titleText;

    [SerializeField] public Text flavorText;
    [SerializeField] public Text choiceText;
    [SerializeField] public Text weekCostText;
    [SerializeField] public Image weekCostImage;
    [SerializeField] public Image illustrationImage;
    [SerializeField] public Transform relativePos;

    [SerializeField] public Image bonusIconImage;

    private Vector3? prevMousePos = null;

    private float initialEffectAlpha;

    const float DISTANCE_EFFECT_SHOW = 50;

    public GameObject followupCardNo = null;
    public GameObject followupCardYes = null;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        if (cardContent != null) {
            flavorText.text = cardContent.situation;
            titleText.text = cardContent.name != null ? cardContent.name.ToUpper() : "";

            // non-breaking spaces between ! and ?
            flavorText.text = flavorText.text.Replace(" ?", " ?").Replace(" !", " !");
            titleText.text = titleText.text.Replace(" ?", " ?").Replace(" !", " !");

            Sprite sprite = Resources.Load<Sprite>("illustrations/" + this.cardContent.image);
            if (sprite != null) {
                illustrationImage.sprite = sprite;
            }

            if (cardContent.bonus) {
                bonusIconImage.gameObject.SetActive(true);
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
        } else if (!swiped && cardEffectCanvas != null && cardContent != null) {
            CardEffect currentEffect = new CardEffect();
            if (cardContent != null) {
                if (relativePos.localPosition.x > 0) {
                    currentEffect = cardContent.yes;
                } else {
                    currentEffect = cardContent.no;
                }
            }
            PreviewEffect(currentEffect, Mathf.Abs(relativePos.localPosition.x) / DISTANCE_EFFECT_SHOW);
        }
    }

    void PreviewEffect(CardEffect effect, float opacity) {
        choiceText.text = effect.choice.ToUpper();
        if (effect.cost != 0) {
            weekCostImage.enabled = true;
            weekCostText.enabled = true;
            weekCostText.text = (-effect.cost).ToString();
        } else {
            weekCostImage.enabled = false;
            weekCostText.enabled = false;
        }
        cardEffectCanvas.alpha = opacity;
        UIManager.Instance.PreviewEffect(effect, opacity);
    }

    public void SwipeYes()
    {
        if (swiped) { return; }
        swiped = true;
        if (onSwipeYes != null) {
            // Prevent memory leak by destroying unused follow up card
            this.cleanFollowUp("no");
            swiped = this.onSwipeYes(cardContent != null ? cardContent.yes : new CardEffect(), true, this.followupCardYes);
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
            // Prevent memory leak by destroying unused follow up card
            this.cleanFollowUp("yes");
            swiped = this.onSwipeNo(cardContent != null ? cardContent.no : new CardEffect(), false, this.followupCardNo);
        }
        if (swiped) {
            StartCoroutine(FinishSwipe());
        }
    }

    void cleanFollowUp(string side) {
        GameObject keep = (side == "yes") ? this.followupCardNo : this.followupCardYes ;
        GameObject remove = (side == "yes") ? this.followupCardYes : this.followupCardNo ;

        if (remove && remove != keep) Destroy(remove);
    }

    IEnumerator FinishSwipe()
    {
        yield return new WaitForSeconds(VANISH_TIME);
        GameObject.Destroy(gameObject);
    }

    public bool isSwiped() {
        return swiped;
    }

    public void setFollowupCard(GameObject cardInstance) {
        this.followupCardYes = cardInstance;
        this.followupCardNo = cardInstance;
    }

    public void setFollowupCardYes(GameObject cardInstance) {
      this.followupCardYes = cardInstance;
    }

    public void setFollowupCardNo(GameObject cardInstance) {
      this.followupCardNo = cardInstance;
    }
}
