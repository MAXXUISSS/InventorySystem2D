using UnityEngine;
using UnityEngine.InputSystem;

public class InteracionDetector : MonoBehaviour
{
    private IInteractable interactableInRange = null; 
    public GameObject interactionIcon;

    void Start()
    {
        interactionIcon.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started) // activates when the key is pressed
        {
            interactableInRange?.Interact();
            Debug.Log(" OnInteract() activated");

            if (interactableInRange != null)
            {
                Debug.Log(" Interacting with: " + interactableInRange);

                if (!interactableInRange.CanInteract())
                {
                    interactionIcon.SetActive(false);
                }
            }
            else
            {
                Debug.LogWarning(" There is no object in range.");
            }
        }
    }






    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            Debug.Log("Interactuable object detected: " + collision.gameObject.name);

            if (interactable.CanInteract())
            {
                interactableInRange = interactable;
                interactionIcon.SetActive(true);
                Debug.Log("you can interact with: " + collision.gameObject.name);
            }
            else
            {
                Debug.Log("you cant interact with: " + collision.gameObject.name);
            }
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IInteractable interactable) && interactable == interactableInRange)
        {
            interactableInRange = null;
            interactionIcon.SetActive(false);
        }
    }
}
