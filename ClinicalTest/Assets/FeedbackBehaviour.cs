using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackBehaviour : MonoBehaviour
{
    [SerializeField] Text text;
    public string feedback;

    // Start is called before the first frame update
    void Start()
    {
        text.text = feedback;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
