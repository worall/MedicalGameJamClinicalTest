using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
{
    private Transform root;
    private CardBehaviour card;
    private Image cardBack;
    private bool flipped = false;
    private bool ready = false;
    private Vector3? prevMousePos = null;

    void Awake()
    {
        root = GetComponentsInParent<Transform>()[1];
        card = GetComponentInParent<CardBehaviour>();
        cardBack = card.GetComponentsInChildren<Image>()[2];
    }

    const float SWIPE_AMPLITUDE = 20;

    void Update()
    {
        if (!flipped && Mathf.Abs(root.localPosition.x) > SWIPE_AMPLITUDE)
        {
            flipped = true;
        }

        if (flipped && Mathf.Abs(root.localPosition.x) < SWIPE_AMPLITUDE / 4)
        {
            ready = true;
        }

        float targetAngle = flipped ? 0 : 180;
        float currentY = this.root.localRotation.eulerAngles.y;
        float angle = currentY + (targetAngle - currentY) * 0.1f;
        this.root.localRotation = Quaternion.Euler(0, angle, 0);
        //angle = Mathf.Clamp(angle, -80, 80);

        float currentAngle = (this.root.localRotation.eulerAngles.y - 90) % 360;
        this.cardBack.color = new Color(1, 0, 0, currentAngle > 0 && currentAngle < 180 ? 1 : 0);
        if (prevMousePos == null && !card.swiped)
        {
            this.root.localPosition = new Vector3(root.localPosition.x * 0.5f, root.localPosition.y * 0.5f, root.localPosition.z * 0.5f);
        }
    }

    void OnMouseDrag()
    {
        Vector3 mousePos = Input.mousePosition;
        if (prevMousePos == null)
        {
            prevMousePos = mousePos;
        }

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

        float amplitude = this.root.localPosition.magnitude;
        if (amplitude > SWIPE_AMPLITUDE && ready)
        {
            if (root.localPosition.x > 0)
            {
                card.SwipeYes();
            }
            else if (root.localPosition.x < 0)
            {
                card.SwipeNo();
            }
        }
    }
}
