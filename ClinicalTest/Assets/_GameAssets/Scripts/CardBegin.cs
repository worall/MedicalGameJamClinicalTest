using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBegin : CardBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {
        if (swiped)
        {
            vanishRatio = Mathf.Min(1, vanishRatio + Time.deltaTime / VANISH_TIME);
        }
        if (vanishRatio > 0)
        {
            float alpha = Mathf.Min(1, 2 - vanishRatio * 2);
            // Image[] images = GetComponentsInChildren<Image>();
            // for (int i = 0; i < images.Length; i++) {
            //     images[i].color = new Color(1, 1, 1, images[i].color.a * alpha);
            // }
            // Text[] texts = GetComponentsInChildren<Text>();
            // for (int i = 0; i < texts.Length; i++) {
            //     texts[i].color = new Color(1, 1, 1, texts[i].color.a * alpha);
            // }
            cardMainCanvas.alpha = alpha;
            vanishOverlay.color = new Color(1, 1, 1, Mathf.Min(1, vanishRatio) * alpha);

            float s = 1 + 0.03f * vanishRatio;
            transform.localScale = new Vector3(s, s, s);
        }
    }

}
