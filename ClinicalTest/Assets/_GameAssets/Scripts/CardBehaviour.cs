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

  private bool swiped = false;

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

  }

  public void SwipeYes() {
    if (swiped) { return; }
    if (onSwipeYes != null) {
      this.onSwipeYes(cardEffectsYes);
      swiped = true;
      StartCoroutine(FinishSwipe());
    }
  }
  public void SwipeNo() {
    if (swiped) { return; }
    if (onSwipeNo != null) {
      this.onSwipeNo(cardEffectsNo);
      swiped = true;
      StartCoroutine(FinishSwipe());
    }
  }

  IEnumerator FinishSwipe() {
    yield return new WaitForSeconds(1);
    GameObject.Destroy(gameObject);
  }
}
