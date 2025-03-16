using System.Collections.Generic;
using UnityEngine;

// we can pack data in a .txt format so we can save thins in json format
[System.Serializable]
public class SaveData
{
    public Vector3 playerPosition;

    public string mapBoundary; // boundary name for the map


    //Inventory data

    public List<InventorySaveData> inventorySaveData;
    public List<InventorySaveData> hotbarSaveData;
    public List<ChestSaveData> chestSaveData;








}

[System.Serializable]

public class ChestSaveData
{
    public string chestID;
    public bool isOpened;

}
