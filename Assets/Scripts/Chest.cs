using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool IsOpened { get; private set; }
    public string ChestID { get; private set; }
    public GameObject itemPrefab; // Item que el cofre suelta
    public Sprite openedSprite;

    void Start()
    {
        ChestID ??= GlobalHelper.GenerateUniqueID(gameObject); // Genera un ID único
    }

    public bool CanInteract()
    {
        Debug.Log(" Verificando si el cofre puede ser abierto: " + !IsOpened);
        return !IsOpened;
    }


    public void Interact()
    {
        Debug.Log(" Intentando interactuar con el cofre: " + ChestID);

        if (!CanInteract())
        {
            Debug.Log(" El cofre ya está abierto.");
            return;
        }

        Debug.Log(" Cofre abierto correctamente");
        OpenChest();
    }


    private void OpenChest()
    {
        SetOpened(true);

        // Generar el objeto si el cofre tiene un itemPrefab
        if (itemPrefab)
        {
            GameObject droppedItem = Instantiate(itemPrefab, transform.position + Vector3.down, Quaternion.identity);
            droppedItem.GetComponent<BounceEffect>().StartBounce();
        }
    }

    public void SetOpened(bool opened)
    {
        if (IsOpened = opened)
        {
            GetComponent<SpriteRenderer>().sprite = openedSprite;
        }
    }
}
