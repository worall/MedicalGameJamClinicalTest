using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : Panel
{
    [SerializeField] private Image m_scienceQualityImage;
    [SerializeField] private Image m_lifeQualityImage;
    [SerializeField] private Image m_patientNumberImage;
    [SerializeField] private Image m_patientVarietyImage;

    public void Init(float baseAmount)
    {
        float amount = baseAmount / 100;
        Debug.Log(amount);

        m_scienceQualityImage.fillAmount = amount;
        m_lifeQualityImage.fillAmount = amount;
        m_patientNumberImage.fillAmount = amount;
        m_patientVarietyImage.fillAmount = amount;

    }

    public void UpdateStats(int scienceQuality, float lifeQuality, float patientNumber, float patientVariety)
    {
        m_scienceQualityImage.fillAmount += scienceQuality;
        m_lifeQualityImage.fillAmount += lifeQuality;
        m_patientNumberImage.fillAmount += patientNumber;
        m_patientVarietyImage.fillAmount += patientVariety;
    }
}
