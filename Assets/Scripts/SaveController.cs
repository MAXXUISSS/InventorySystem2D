using System.IO;
using Unity.Cinemachine;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    void Start()
    {
        //Define where you want to save location
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        LoadGame();

    }

    public void SaveGame()
    {
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            mapBoundary = FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D.gameObject.name
        };

        string json = JsonUtility.ToJson(saveData);
        File.WriteAllText(saveLocation, json);
        Debug.Log("Game saved at: " + saveLocation);
    }


    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));

            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

            FindFirstObjectByType<CinemachineConfiner2D>().BoundingShape2D = GameObject.Find(saveData.mapBoundary).GetComponent<PolygonCollider2D>();

            Debug.Log("Game Loaded!");
        }
        else
        {
            Debug.Log("No save file found. Creating new save.");
            SaveGame();
        }
    }

}
