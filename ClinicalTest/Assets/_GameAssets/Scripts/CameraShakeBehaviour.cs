using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShakeBehaviour : MonoBehaviour
{
    float shakeRatio = 0;
    Vector3 originalPos;

    // Start is called before the first frame update
    void Start()
    {
        originalPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeRatio > 0.001) {
            Vector2 offset = Random.insideUnitCircle * 24 * shakeRatio;
            this.transform.position = new Vector3(
                originalPos.x + offset.x * 2,
                originalPos.y + offset.y,
                originalPos.z);
            shakeRatio *= 0.75f;
        } else {
            this.transform.position = originalPos;
        }

    }

    public void Shake() {
        shakeRatio = 1;
    }
}
