using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class ItemDictionary : MonoBehaviour
{
    public List<Item> itemPrefabs;
    private Dictionary<int, GameObject> itemDictionary;


    private void Awake()// we use awake beacuse we want the dictionary to charge before anything else
    {
        itemDictionary = new Dictionary<int, GameObject>();

        //AutoincrementIds
        for(int i = 0; i < itemPrefabs.Count; i++)
        {
            if (itemPrefabs[i] != null)
            {
                itemPrefabs[i].ID = i + 1;

            }
        }
        foreach(Item item in itemPrefabs)
        {
            itemDictionary[item.ID] = item.gameObject;
        }
    }

    public GameObject GetItemPrefab(int itemID)
    {
        itemDictionary.TryGetValue(itemID, out GameObject prefab);
        if(prefab == null)
        {
            Debug.LogWarning("Item with ID itemID no found in dictionary");
        }
        return prefab;


    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
