using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using System.Collections;

public class ItemPickupUiController : MonoBehaviour
{
    // Singleton para asegurar una sola instancia
    public static ItemPickupUiController Instance { get; private set; }

    public GameObject popupPrefab; // Referencia al prefab de popup
    public int maxPopups = 3; // Límite máximo de popups activos
    public float popupDuration = 3f; // Duración de cada popup

    private readonly Queue<GameObject> activePopups = new();

    private void Awake()
    {
        Debug.Log("Instancia creada desde: " + gameObject.name);

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple ItemPickUpUIManager instances detected! Destroying the extra one");
            Destroy(gameObject);
        }
    }

    public void ShowItemPickup(string itemName, Sprite itemIcon)
    {
        // Instanciar el popup como hijo del controlador UI
        GameObject newPopup = Instantiate(popupPrefab, transform);

        // Obtener el texto y asignarle el nombre del ítem
        newPopup.GetComponentInChildren<TMP_Text>().text = itemName;

        // Obtener la imagen y asignar el icono
        Image itemImage = newPopup.transform.Find("ItemIcon")?.GetComponent<Image>();
        if (itemImage != null)
        {
            itemImage.sprite = itemIcon;
        }

        // Agregar el popup a la cola
        activePopups.Enqueue(newPopup);

        // Si hay más popups que el límite, destruir el más antiguo
        if (activePopups.Count > maxPopups)
        {
            Destroy(activePopups.Dequeue());
        }

        // Comenzar la rutina para desvanecer y destruir el popup
        StartCoroutine(FadeOutAndDestroy(newPopup));
    }

    private IEnumerator FadeOutAndDestroy(GameObject popup)
    {
        yield return new WaitForSeconds(popupDuration);

        if (popup == null) yield break;

        CanvasGroup canvasGroup = popup.GetComponent<CanvasGroup>();

        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Time to fade away
            canvasGroup.alpha = Mathf.Lerp(3.4f, 0.1f, elapsedTime / fadeDuration);
            yield return null;
        }

        Destroy(popup);
    }

}