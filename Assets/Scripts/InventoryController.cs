using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    //Reference to item Dictionary

    private ItemDictionary itemDictionary;

    //Inventory Control
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;
    

    void Start()
    {

        itemDictionary = FindFirstObjectByType<ItemDictionary>();
        //for (int i = 0; i < slotCount; i++)
        //{
        //    Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
        //    if(i < itemPrefabs.Length)
        //    {
        //        GameObject item = Instantiate(itemPrefabs[i], slot.transform);
        //        item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //        slot.currentItem = item;
        //    }
        //}
    }

    public bool AddItem(GameObject itemPrefab)
    {
        //Look for an empty slot
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(itemPrefab, slot.transform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }

        }
        //Here you can add an animation or a message in screen so the player  could see that the inventory is full
        Debug.Log("Inventory is full");
        return false;
    }
    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });// with getSibligbIndex you get the index of a game object in conjunction to other game objects in the same level  
            }
        }
        return invData;
    }

    public void SetInventoryItems(List<InventorySaveData> inventorySaveData)
    {
        // clear inventory panel and avoid duplicates

        foreach(Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //create new slots

        for(int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }

        //Populate slot with saved items
        foreach(InventorySaveData data in inventorySaveData)
        {
            if(data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);

                if(itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }
    }
    
}
