using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackBehaviour : MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] Image patientsUpImage;
    [SerializeField] Image patientsDownImage;
    [SerializeField] Image rigueurUpImage;
    [SerializeField] Image rigueurDownImage;
    [SerializeField] Image implicationUpImage;
    [SerializeField] Image implicationDownImage;
    [SerializeField] Image argentUpImage;
    [SerializeField] Image argentDownImage;
    public CardEffect effect;

    // Start is called before the first frame update
    void Start()
    {
        text.text = effect.debrief;
        patientsUpImage.enabled = effect.patients > 0;
        patientsDownImage.enabled = effect.patients < 0;
        rigueurUpImage.enabled = effect.rigueur > 0;
        rigueurDownImage.enabled = effect.rigueur < 0;
        implicationUpImage.enabled = effect.implication > 0;
        implicationDownImage.enabled = effect.implication < 0;
        argentUpImage.enabled = effect.argent > 0;
        argentDownImage.enabled = effect.argent < 0;
    }
}
