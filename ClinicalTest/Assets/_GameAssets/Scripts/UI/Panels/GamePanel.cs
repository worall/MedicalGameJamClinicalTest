﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    [SerializeField] private Image m_scienceQualityImage;
    [SerializeField] private Image m_patientImplicationImage;
    [SerializeField] private Image m_patientNumberImage;
    [SerializeField] private Image m_moneyImage;
    [SerializeField] private Image m_imageTest;

    public void Init(float baseAmount)
    {
        float amount = baseAmount / 100;
        Debug.Log(amount);

        m_scienceQualityImage.fillAmount = amount;
        m_patientImplicationImage.fillAmount = amount;
        m_patientNumberImage.fillAmount = amount;
        m_moneyImage.fillAmount = amount;

    }

    public void UpdateStats(int scienceQuality, float patientImplication, float patientNumber, float money)
    {
        m_scienceQualityImage.fillAmount += scienceQuality;
        m_patientImplicationImage.fillAmount += patientImplication;
        m_patientNumberImage.fillAmount += patientNumber;
        m_moneyImage.fillAmount += money;
    }
}
