using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndCardBehaviour : MonoBehaviour
{
    public int validStars = 0;
    [SerializeField] public Image[] stars;
    [SerializeField] public Text text;

    private float startTime;

    void Awake()
    {
        startTime = Time.time;
    }

    void Start() {
        if (validStars < 1) {
            text.text = "Malheureusement votre essai n'est pas aussi concluant que prévu, essayez de faire mieux la prochaine fois !";
        } else if (validStars < 2) {
            text.text = "Votre essai est assez satisfaisant mais il reste certaines incertitudes suite à ce test, vous êtes capable de faire bien mieux !";
        } else if (validStars < 3) {
            text.text = "Félicitations votre essai est une totale réussite ! Peu de chercheurs arrivent à de tels résultats, continuez comme ça !";
        }
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
