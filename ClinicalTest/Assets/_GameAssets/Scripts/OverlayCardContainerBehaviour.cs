using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayCardContainerBehaviour : MonoBehaviour
{
    // change this to control the card that will be displayed in the overlay
    [SerializeField] public CardBehaviour cardPrefab;
    private CardBehaviour cardInstance;

    [SerializeField] public GameObject cardRoot;

    private Canvas backdropCanvas;
    CanvasGroup backdropCanvasGroup;

    const float FADE_TIME = 0.2f; // seconds
    private bool fadingIn = true;
    private float startTime;


    // Start is called before the first frame update
    void Start()
    {
        backdropCanvas = GetComponentInChildren<Canvas>();
        backdropCanvasGroup = backdropCanvas.GetComponent<CanvasGroup>();

        // set camera for backdrop canvas
        backdropCanvas.worldCamera = Camera.main;

        backdropCanvasGroup.alpha = 0;
        startTime = Time.time;

        if (cardPrefab == null) {
            throw new System.Exception("Card Prefab non défini");
        }
        cardInstance = Instantiate(cardPrefab);
        cardInstance.transform.SetParent(cardRoot.transform, false);
        cardInstance.relativePos.localRotation = Quaternion.Euler(0, 0, 0);
        cardInstance.relativePos.localPosition = new Vector3(400, 700, 0);

        // this is to make sure the card canvases are rendered on top of the backdrop
        Canvas[] canvases = cardInstance.GetComponentsInChildren<Canvas>();
        foreach (Canvas canvas in canvases) {
            canvas.sortingOrder += 11;
        }

        cardInstance.onSwipeNo = CloseOverlay;
        cardInstance.onSwipeYes = CloseOverlay;
    }

    // Update is called once per frame
    void Update()
    {
        float alpha = Mathf.Min(1, (Time.time - startTime) / FADE_TIME);
        backdropCanvasGroup.alpha = fadingIn ? alpha : 1 - alpha;

        if (!fadingIn && Time.time >= startTime + FADE_TIME) {
            Destroy(gameObject);
        }
    }

    bool CloseOverlay(CardEffect effect, bool swipedRight) {
        fadingIn = false;
        startTime = Time.time;
        return true;
    }
}
