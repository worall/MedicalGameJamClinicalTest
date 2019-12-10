using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
{
    [SerializeField] public Transform root;
    private CardBehaviour card;
    private Vector3? prevMousePos = null;

    int isSwiping = 0; // 1 is right (yes), -1 is left (no)

    [SerializeField] public Image cardBack;

    [SerializeField] public CanvasGroup cardEffectCanvas;

    float dampenedDiff = 0;

    void Awake()
    {
        card = GetComponentInParent<CardBehaviour>();
    }

    // min speed to get a swipe
    const float SWIPE_SPEED = 120;

    void Update()
    {
        float targetAngle = 0;
        float currentY = this.root.localRotation.eulerAngles.y;
        float angle = currentY + (targetAngle - currentY) * 0.08f;
        // this.root.localRotation = Quaternion.Euler(0, angle, 0);
        //angle = Mathf.Clamp(angle, -80, 80);

        float currentAngle = (this.root.localRotation.eulerAngles.y - 90) % 360;
        this.cardBack.color = new Color(1, 1, 1, currentAngle > 0 && currentAngle < 180 ? 1 : 0);
        if (prevMousePos == null && !card.isSwiped())
        {
            float restPos = card.floating ? Mathf.Sin(Time.time) * 60 : 0;
            Vector3 restPosition = new Vector3(restPos, -Mathf.Abs(restPos * 0.3f), 0);
            this.root.localPosition += (restPosition - this.root.localPosition) * 0.3f;
        }

        // rotation
        this.root.localRotation = Quaternion.Euler(0, angle, Mathf.Clamp(this.root.localPosition.x * -0.012f, -8, 8));
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        if (prevMousePos == null)
        {
            prevMousePos = mousePos;
        }

        float diff = Input.mousePosition.x - prevMousePos.Value.x;

        if (Mathf.Abs(diff) > Mathf.Abs(dampenedDiff)) {
            dampenedDiff = diff;
        } else {
            dampenedDiff = Mathf.Lerp(dampenedDiff, diff, 0.3f);
        }
        isSwiping = Mathf.Abs(dampenedDiff) > SWIPE_SPEED ? (int)Mathf.Sign(dampenedDiff) : 0;

        Camera cam = Camera.main;
        float camDistance = cam.WorldToScreenPoint(root.position).z;
        Vector3 newWorldPoint = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, camDistance));
        Vector3 prevWorldPoint = cam.ScreenToWorldPoint(new Vector3(((Vector3)prevMousePos).x, ((Vector3)prevMousePos).y, camDistance));
        this.root.localPosition += newWorldPoint - prevWorldPoint;
        prevMousePos = mousePos;
    }

    void OnMouseUp()
    {
        prevMousePos = null;

        if (isSwiping == 1)
        {
            card.SwipeYes();
        }
        else if (isSwiping == -1)
        {
            card.SwipeNo();
        }
    }
}
