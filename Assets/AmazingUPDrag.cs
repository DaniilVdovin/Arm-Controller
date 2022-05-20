using UnityEngine;
using UnityEngine.EventSystems;

public class AmazingUPDrag : MonoBehaviour,
               IBeginDragHandler, IDragHandler, IEndDragHandler
{
    Vector2 dragOffset = Vector2.zero;
    Vector2 limits = Vector2.zero;

    public void OnBeginDrag(PointerEventData eventData)
    {
        dragOffset = eventData.position - (Vector2)transform.position;
        limits = transform.parent.GetComponent<RectTransform>().rect.max;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position - dragOffset;
        var p = transform.localPosition;
        if (p.x < -limits.x) { p.x = -limits.x; }
        if (p.x > limits.x) { p.x = limits.x; }
        if (p.y < -limits.y) { p.y = -limits.y; }
        if (p.y > limits.y) { p.y = limits.y; }
        transform.localPosition = p;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        dragOffset = Vector2.zero;
    }
}