using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum JaugeType {
    RIGUEUR,
    PATIENTS,
    IMPLICATION,
    ARGENT
}

public class JaugeInfoCardBehaviour : MonoBehaviour
{
    string jaugeName;
    string jaugeDescription;

    public JaugeType jaugeType;

    [SerializeField] Text jaugeNameText;
    [SerializeField] Text jaugeDescriptionText;
    [SerializeField] Image rigueurImage;
    [SerializeField] Image patientsImage;
    [SerializeField] Image implicationImage;
    [SerializeField] Image argentImage;

    void Awake() {
        switch(jaugeType) {
            case JaugeType.ARGENT:
                jaugeName = "FINANCEMENT";
                jaugeDescription = "Ce sont les dépenses engendrées tout au long de l'essai.";
                break;
            case JaugeType.IMPLICATION:
                jaugeName = "IMPLICATION DES PATIENTS";
                jaugeDescription = "À quel point l'essai donne envie aux patients de s'engager et à quel point ce qu'on leur demande est acceptable.";
                break;
            case JaugeType.PATIENTS:
                jaugeName = "NOMBRE DE PATIENTS";
                jaugeDescription = "C'est le nombre de patients qui acceptent de participer.";
                break;
            case JaugeType.RIGUEUR:
                jaugeName = "RIGUEUR SCIENTIFIQUE";
                jaugeDescription = "C'est ce qui permet de garantir la qualité des résultats finaux de l'essai.";
                break;
        }
    }

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
