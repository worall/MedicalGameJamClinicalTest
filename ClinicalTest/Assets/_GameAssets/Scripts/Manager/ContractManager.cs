using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    private static ContractManager _instance;
    public static ContractManager Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    [SerializeField] private List<Contract> contractsList;

    public Contract actualContract;
    private int index = 0;

    public void SelectNextContrat()
    {
        actualContract = contractsList[index];
    }

    private void OnContractEnded()
    {
        if (index < contractsList.Count)
            index++;

        else
            Debug.Log("fin des contrats");
    }

}

[System.Serializable]
public struct Contract
{
    public int implicationRequirement;
    public int scienceQualityRequirement;
    public int patientNumberRequiremenent;

    public int maxPatientNumber;
    public int timeToConclude;
    public int baseValue;
}
