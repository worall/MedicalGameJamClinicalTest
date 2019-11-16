using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CardBehaviour card = GameObject.Find("CardRoot").GetComponent<CardBehaviour>();
        card.onSwipeNo = dummySwipeY;
        card.onSwipeYes = dummySwipeN;
    }

    void dummySwipeY(CardBehaviour.CardEffects effects)
    {
        print("YES!!");
    }
    void dummySwipeN(CardBehaviour.CardEffects effects)
    {
        print("NO");
    }
}
