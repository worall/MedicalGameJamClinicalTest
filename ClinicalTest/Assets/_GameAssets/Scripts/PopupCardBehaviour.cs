using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCardBehaviour : MonoBehaviour
{
    [SerializeField] public PopupCardContent popupContent;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Is this real life ?");
        Debug.Log(popupContent.title);
        Debug.Log(popupContent.description);
        Debug.Log(popupContent.options);

        Text title = this.GetComponentsInChildren<Text>()[0];
        Text desc = this.GetComponentsInChildren<Text>()[1];
        Text options = this.GetComponentsInChildren<Text>()[2];

        title.text = popupContent.title;
        desc.text = popupContent.description;
        options.text = popupContent.options;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
