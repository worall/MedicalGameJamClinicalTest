using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutoCardBehaviour : MonoBehaviour
{
    public int currentStep = 0;

    public const int MAX_STEP = 4;

    [SerializeField] Text titleText;
    [SerializeField] Text descriptionText;
    [SerializeField] GameObject myPrefab;

    // Start is called before the first frame update
    void Start()
    {
        switch(currentStep) {
            case 0:
                titleText.text = "Bienvenue !";
                descriptionText.text = "Bienvenue dans King of Trial ! Faites glisser la carte à gauche ou à droite afin de continuer !";
                break;
            case 1:
                titleText.text = "Votre objectif !";
                descriptionText.text = "Votre objectif est de mener à bien un essai clinique ! Dans ce jeu vous vous vos patients seront les adorables \"Catipat's\" !";
                break;
            case 2:
                titleText.text = "Comment jouer ?";
                descriptionText.text = "Vous ferez un choix différent si vous glissez la carte à droite ou à gauche ! Faites les bons choix afin d'équilibrer la qualité de l'étude, le nombre de catipat's, leur implication et votre budget !";
                break;
            case 3:
                titleText.text = "Quelques conseils !";
                descriptionText.text = "Vous pouvez appuyer sur le logo des jauges afin d'obtenir des informations complémentaires. Et ne vous inquiètez pas si vous ne réussissez pas parfaitement votre étude du premier coup, vous êtes là pour apprendre !";
                break;
            case 4:
                titleText.text = "A vous de jouer !";
                descriptionText.text = "Vous connaissez maintenant les bases du jeu ! Faites glissez la carte afin de recevoir votre premier contrat. Bonne chance !";
                break;
        }

        if (currentStep < MAX_STEP) {
            GameObject next = Instantiate(this.myPrefab);
            next.SetActive(false);
            next.GetComponent<TutoCardBehaviour>().currentStep = this.currentStep + 1;
            this.GetComponent<CardBehaviour>().setFollowupCard(next);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
