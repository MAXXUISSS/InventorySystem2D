using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDragHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{

    Transform originalParent;// if we throw the item out of the inventory it will snap back to its place
    CanvasGroup canvasGroup;

    //Distance for item to drop

    public float minDropDistance = 2f;
    public float maxDropDistance = 3f;


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

    public void OnEndDrag(PointerEventData eventData )
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
            //is a slot under drop point
            if (dropSlot.currentItem != null)
            {
                //slot has an item - swap items
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
            // if where we're droppin is not within the inventory
           
            if (!IsWhitinInventory(eventData.position))
            {
                // Drop our item
                DropItem(originalSlot);
            }
            else
            {
                //Snap back to the og slot
                transform.SetParent(originalParent);
            }


                
                
        }

        GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // Center
    }

    bool IsWhitinInventory(Vector2 mousePosition)
    {
        RectTransform inventoryRect = originalParent.parent.GetComponent<RectTransform>();
        return RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, mousePosition);//check if the mouse is inside the rectangle inventory


    }

    void DropItem(Slot originalSlot)
    {
        originalSlot.currentItem = null;

        //FindPlayer
        Transform playerTransform = GameObject.FindGameObjectWithTag("Player" )?.transform;
        if(playerTransform == null)
        {
            Debug.LogError("Missing 'Player' tag");
            return;
        }

        //Random drop position
        Vector2 dropOffSet = Random.insideUnitCircle.normalized * Random.Range(minDropDistance, maxDropDistance);
        Vector2 dropPosition =(Vector2)playerTransform.position + dropOffSet;

        //Instantiate drop item and bounce

        GameObject dropItem = Instantiate(gameObject, dropPosition, Quaternion.identity);
        dropItem.GetComponent<BounceEffect>().StartBounce();
        //Destroy the UI one

        Destroy(gameObject);
    }



}


