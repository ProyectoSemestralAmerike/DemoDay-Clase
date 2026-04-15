using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class PlayerInteractableDetector : MonoBehaviour
{
    bool canDetect = true;

    public bool canInteract = true;
    InputAction interactAction;

    Interactable currentInteractable;

    [SerializeField] TextMeshProUGUI InteractPrompt;
    [SerializeField] Canvas InteractCanvas;
    PlayerController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InteractCanvas.gameObject.SetActive(false);
        interactAction = InputSystem.actions.FindAction("Interact");
        interactAction.performed += HandleInteractAction;
        controller = GetComponent<PlayerController>();
    }

    void HandleInteractAction(InputAction.CallbackContext context)
    {
        Debug.Log("Interact action performed");
        if (canInteract && currentInteractable != null)
        {
            Debug.Log("Interact with " + currentInteractable.Name);
            currentInteractable.Interact();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Trigger");

        if (canDetect == false)
            return;

        if (other.tag == "Interactable" && currentInteractable == null)
        {
            Debug.Log("Interactable found");
            currentInteractable = other.GetComponent<Interactable>();
            if (currentInteractable.State != InteractableState.Hidden)
            {
                ShowInteractUI();
            }
            else
            {
                currentInteractable = null;
                HideInteractUI();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (currentInteractable != null && other.tag == "Interactable")
        {
            if (currentInteractable.gameObject == other.gameObject)
            {
                currentInteractable = null;
                HideInteractUI();
            }
        }
    }

    void ShowInteractUI()
    {
        InteractCanvas.gameObject.SetActive(true);
        InteractPrompt.text = currentInteractable.GetInteractPrompt();


    }

    void HideInteractUI()
    {
        InteractCanvas.gameObject.SetActive(false);
        InteractPrompt.text = "";
    }
}
