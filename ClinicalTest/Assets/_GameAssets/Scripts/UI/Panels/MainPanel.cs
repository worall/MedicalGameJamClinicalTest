using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel : Panel
{
    public event Action OnButtonStartGame;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnButtonStartClick()
    {
        OnButtonStartGame?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            Debug.Log("coucou");
        }
    }

}
