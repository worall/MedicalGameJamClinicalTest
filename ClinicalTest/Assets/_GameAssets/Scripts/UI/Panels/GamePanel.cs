using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    [SerializeField] private Image m_patientImplicationImage;
    [SerializeField] private Image m_patientNumberImage;
    [SerializeField] private Image m_scienceQualityImage;
    [SerializeField] private Image m_moneyImage;
    [SerializeField] private Text m_timerText;

    [SerializeField] private Color fullFillColor;
    [SerializeField] private Color fillColor;

    public void Init(float baseAmount)
    {
        float amount = baseAmount / 100;

        m_scienceQualityImage.fillAmount = amount;
        m_patientImplicationImage.fillAmount = amount;
        m_patientNumberImage.fillAmount = amount;
        m_moneyImage.fillAmount = amount;
    }

    public void UpdateStats(float scienceQuality, float patientImplication, float patientNumber, float money, int time)
    {

        m_scienceQualityImage.fillAmount = scienceQuality / 100;
        m_patientImplicationImage.fillAmount = patientImplication / 100;
        m_patientNumberImage.fillAmount = patientNumber / 100;
        m_moneyImage.fillAmount = money / 100;
        m_timerText.text = time.ToString();

        if (m_scienceQualityImage.fillAmount >= 1)
            m_scienceQualityImage.color = fullFillColor;
        else
            m_scienceQualityImage.color = fillColor;

        if (m_patientImplicationImage.fillAmount >= 1)
            m_patientImplicationImage.color = fullFillColor;
        else
            m_patientImplicationImage.color = fillColor;

        if (m_patientNumberImage.fillAmount >= 1)
            m_patientNumberImage.color = fullFillColor;
        else
            m_patientNumberImage.color = fillColor;

        if (m_moneyImage.fillAmount >= 1)
            m_moneyImage.color = fullFillColor;
        else
            m_moneyImage.color = fillColor;
    }
}
