using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JaugeInfoCardBehaviour : MonoBehaviour
{
    public string jaugeName;
    public string jaugeDescription;

    public enum JaugeType {
        RIGUEUR,
        PATIENTS,
        IMPLICATION,
        ARGENT
    }
    public JaugeType jaugeType;

    [SerializeField] Text jaugeNameText;
    [SerializeField] Text jaugeDescriptionText;
    [SerializeField] Image rigueurImage;
    [SerializeField] Image patientsImage;
    [SerializeField] Image implicationImage;
    [SerializeField] Image argentImage;

    // Start is called before the first frame update
    void Start()
    {
        jaugeNameText.text = jaugeName;
        jaugeDescriptionText.text = jaugeDescription;
        rigueurImage.enabled = jaugeType == JaugeType.RIGUEUR;
        patientsImage.enabled = jaugeType == JaugeType.PATIENTS;
        implicationImage.enabled = jaugeType == JaugeType.IMPLICATION;
        argentImage.enabled = jaugeType == JaugeType.ARGENT;
    }
}
