using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider))]
public class PlayerInteractableDetector : MonoBehaviour
{
    bool canDetect = true;

    public bool canInteract = true;

    Interactable currentInteractable;

    [SerializeField] TextMeshProUGUI InteractPrompt;
    [SerializeField] Canvas InteractCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        InteractCanvas.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (canDetect == false)
            return;

        if (other.tag == "Interactable" && currentInteractable == null)
        {
            // Debug.Log("Interactable found");
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

    private void OnTriggerExit(Collider other)
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
        InteractPrompt.text = currentInteractable.Label;


    }

    void HideInteractUI()
    {
        InteractCanvas.gameObject.SetActive(false);
        InteractPrompt.text = "";
    }
}
