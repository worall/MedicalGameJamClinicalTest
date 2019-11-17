using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDrag : MonoBehaviour
{
    [SerializeField] public Transform root;
    private CardBehaviour card;
    private bool flipped = true;
    private bool ready = false;
    private Vector3? prevMousePos = null;

    [SerializeField] public Image cardBack;

    [SerializeField] public CanvasGroup cardEffectCanvas;

    void Awake()
    {
        card = GetComponentInParent<CardBehaviour>();
    }

    const float SWIPE_AMPLITUDE = 30;

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
        float angle = currentY + (targetAngle - currentY) * 0.08f;
        // this.root.localRotation = Quaternion.Euler(0, angle, 0);
        //angle = Mathf.Clamp(angle, -80, 80);

        float currentAngle = (this.root.localRotation.eulerAngles.y - 90) % 360;
        this.cardBack.color = new Color(1, 1, 1, currentAngle > 0 && currentAngle < 180 ? 1 : 0);
        if (prevMousePos == null && !card.swiped)
        {
            float restPos = card.floating ? Mathf.Sin(Time.time) * 60 : 0;
            Vector3 restPosition = new Vector3(restPos, -Mathf.Abs(restPos * 0.3f), 0);
            //this.root.localPosition = new Vector3(root.localPosition.x * 0.5f, root.localPosition.y * 0.5f, root.localPosition.z * 0.5f);
            this.root.localPosition += (restPosition - this.root.localPosition) * 0.3f;
        }

        // rotation
        this.root.localRotation = Quaternion.Euler(0, angle, Mathf.Clamp(this.root.localPosition.x * -0.012f, -8, 8));

        if (!card.swiped) {
            cardEffectCanvas.alpha = Mathf.Abs(root.localPosition.x) / SWIPE_AMPLITUDE * 0.65f;
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
