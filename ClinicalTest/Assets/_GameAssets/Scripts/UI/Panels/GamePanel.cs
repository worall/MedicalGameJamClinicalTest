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

    public void Init(float baseAmount)
    {
        float amount = baseAmount / 100;

        m_scienceQualityImage.fillAmount = amount;
        m_patientImplicationImage.fillAmount = amount;
        m_patientNumberImage.fillAmount = amount;
        m_moneyImage.fillAmount = amount;

    }

    public void UpdateStats(int scienceQuality, float patientImplication, float patientNumber, float money)
    {
        m_scienceQualityImage.fillAmount += scienceQuality / 100;
        m_patientImplicationImage.fillAmount += patientImplication / 100;
        m_patientNumberImage.fillAmount += patientNumber / 100;
        m_moneyImage.fillAmount += money / 100;
    }
}
