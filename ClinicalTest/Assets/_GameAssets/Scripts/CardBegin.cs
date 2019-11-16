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

    IEnumerator FinishSwipe()
    {
        yield return new WaitForSeconds(VANISH_TIME);
        GameObject.Destroy(gameObject);
    }

}
