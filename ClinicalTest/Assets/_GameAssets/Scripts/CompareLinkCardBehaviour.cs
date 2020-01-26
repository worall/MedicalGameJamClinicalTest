using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompareLinkCardBehaviour : MonoBehaviour
{
    void Awake() {
        CardBehaviour behaviour = this.GetComponent<CardBehaviour>();
        behaviour.cardContent = new CardContent();
        behaviour.cardContent.yes.choice = "Ouvrir le site internet de COMPARE";
        behaviour.cardContent.no.choice = "Continuer le jeu";
    }

    void Start() {
    }
}
