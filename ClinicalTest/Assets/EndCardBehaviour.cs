using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCardBehaviour : MonoBehaviour
{
    public int validStars = 0;
    [SerializeField] public Image[] stars;

    private float startTime;

    void Awake()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < stars.Length; i++) {
            if (validStars > i) {
                stars[i].color = new Color(1, 1, 1, Mathf.Clamp((Time.time - startTime - i * 0.3f - 1) * 3, 0, 1));
            }
        }
    }
}
