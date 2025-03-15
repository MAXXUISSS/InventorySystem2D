using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Transform originalParent;// if we throw the item out of the inventory it will snap back to its place
    CanvasGroup canvasGroup;


    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        originalParent = transform.parent;//Save parent
        transform.SetParent(transform.root);// Above other canvas
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f;// Semi Transparent during drag
    }

    public void OnDrag(PointerEventData eventData)
    {
        // While we drag it this makes the item follows the mouse
        transform.position = eventData.position; 
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;//enable raycasts
        canvasGroup.alpha = 1f;// no longer transparent

        Slot dropSlot = eventData.pointerEnter?.GetComponent<Slot>();
        Slot originalSlot = originalParent?.GetComponent<Slot>();

        if (dropSlot == null)
        {
            GameObject dropItem = eventData.pointerEnter;
            if (dropItem != null)
            {
                dropSlot = dropItem.GetComponentInParent<Slot>();
            }
        }

        if (dropSlot != null)
        {
            if (dropSlot.currentItem != null)
            {
                dropSlot.currentItem.transform.SetParent(originalSlot?.transform);
                if (originalSlot != null)
                {
                    originalSlot.currentItem = dropSlot.currentItem;
                }
                dropSlot.currentItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            }
            else if (originalSlot != null)
            {
                originalSlot.currentItem = null;
            }

            transform.SetParent(dropSlot.transform);
            dropSlot.currentItem = gameObject;
        }
        else
        {
            transform.SetParent(originalParent);
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center
    }



}


