using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardShadowBehaviour : MonoBehaviour
{
    [SerializeField] public Transform relativePos;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float shadowDist = 20;
        float angleZ = relativePos.rotation.eulerAngles.z * Mathf.Deg2Rad;
        float offsetX = -Mathf.Sin(angleZ) * shadowDist;
        float offsetY = -Mathf.Cos(angleZ) * shadowDist;
        transform.localPosition = new Vector3(offsetX, offsetY, 0);
    }
}
