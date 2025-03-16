using Unity.VisualScripting;
using UnityEngine;

public class PlayerItemCollector : MonoBehaviour
{
    private InventoryController inventoryController;
    void Start()
    {
        inventoryController = FindFirstObjectByType<InventoryController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // When i collide with this item i will add it to my inventory
        if (collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            if (item != null)
            {
                //Add item inventory
                bool itemAdded = inventoryController.AddItem(collision.gameObject);
                if (itemAdded)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
