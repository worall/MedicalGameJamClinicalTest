﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupCardBehaviour : MonoBehaviour
{
    [SerializeField] public PopupCardContent popupContent;
    [SerializeField] public Text titleText;
    [SerializeField] public Text descriptionText;
    [SerializeField] public Text swipeNoText;
    [SerializeField] public Text swipeYesText;

    void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = popupContent.title;
        descriptionText.text = popupContent.description;
        swipeNoText.text = popupContent.swipeNoLabel;
        swipeYesText.text = popupContent.swipeYesLabel;
    }

    // Update is called once per frame
    void Update()
    {
    }

}
