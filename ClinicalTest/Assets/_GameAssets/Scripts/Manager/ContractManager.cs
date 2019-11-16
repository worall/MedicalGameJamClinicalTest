using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContractManager : MonoBehaviour
{
    private static ContractManager _instance;
    public static ContractManager Instance { get { return _instance; } }

    [SerializeField] private List<Contract> contractsList;
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
            return;
        }

        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
