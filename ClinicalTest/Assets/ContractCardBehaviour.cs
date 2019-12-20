using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContractCardBehaviour : MonoBehaviour
{
    public Contract currentContract;

    [SerializeField] Text titleText;
    [SerializeField] Text flavorText;
    [SerializeField] Text implicationRequirementText;
    [SerializeField] Text patientsRequirementText;
    [SerializeField] Text rigueurRequirementText;

    // Start is called before the first frame update
    void Start()
    {
        titleText.text = ("Contrat :\n" + currentContract.title).ToUpper();
        flavorText.text = currentContract.flavorText;
        implicationRequirementText.text = currentContract.implicationRequirement + " %";
        patientsRequirementText.text = currentContract.patientNumberRequiremenent + " %";
        rigueurRequirementText.text = currentContract.scienceQualityRequirement + " %";
    }
}
