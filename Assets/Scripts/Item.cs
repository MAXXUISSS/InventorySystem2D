using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string Name;

    public virtual void PickUp() // virtual means that this method can be overwritten by a child class that inherits from this class
    {
        Sprite itemIcon = GetComponent<Image>().sprite;
        if(ItemPickupUiController.Instance != null)
        {
            ItemPickupUiController.Instance.ShowItemPickup(Name, itemIcon);
        }
    }
}
