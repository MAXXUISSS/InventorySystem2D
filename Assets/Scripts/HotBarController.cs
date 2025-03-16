using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class HotBarController : MonoBehaviour
{
    public GameObject hotbarPanel;
    public GameObject slotPrefab;
    public int slotCount = 10;//1-0 on the keyboard

    private ItemDictionary itemDictionary;

    private Key[] hotbarKeys;

    private void Awake()
    {
        itemDictionary = FindFirstObjectByType<ItemDictionary>();

        hotbarKeys = new Key[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            // Slot 0 is the first slot on the hotbar, we need to set it on Key.Digit1 for 1
            //slot 9 is Digit0
            hotbarKeys[i] = i < 9 ? (Key)((int)Key.Digit1 + i) : Key.Digit0; // Slot 0 is the first slot on the hotbar, we need to set it on Key.Digit1 for 1
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slotCount; i++)
        {
            if (Keyboard.current[hotbarKeys[i]].wasPressedThisFrame)
            {
                //UseItem
                UseItemInSlot(i);

            }
        }
    }

    void UseItemInSlot(int index)
    {
        Slot slot = hotbarPanel.transform.GetChild(index).GetComponent<Slot>();
        if(slot.currentItem != null)
        {
            Item item = slot.currentItem.GetComponent<Item>();
            item.UseItem();
        }
        
    }

    public List<InventorySaveData> GetHotbarItems()
    {
        List<InventorySaveData> hotData = new List<InventorySaveData>();
        foreach (Transform slotTransform in hotbarPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if (slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                hotData.Add(new InventorySaveData { itemID = item.ID, slotIndex = slotTransform.GetSiblingIndex() });// with getSibligbIndex you get the index of a game object in conjunction to other game objects in the same level  
            }
        }
        return hotData;
    }

    public void SetHotbarItems(List<InventorySaveData> inventorySaveData)
    {
        // clear inventory panel and avoid duplicates

        foreach (Transform child in hotbarPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //create new slots

        for (int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, hotbarPanel.transform);
        }

        //Populate slot with saved items
        foreach (InventorySaveData data in inventorySaveData)
        {
            if (data.slotIndex < slotCount)
            {
                Slot slot = hotbarPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemID);

                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }
    }
}
