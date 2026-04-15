using UnityEngine;

public class PickableItemInteractable : Interactable
{
    [SerializeField]
    int quantity;

    [SerializeField]
    string itemName;

    public override void Interact()
    {
        GameManager.Instance.AddItemToInventory(itemName, quantity);
        EndInteraction();
    }

    protected override void EndInteraction()
    {
        Destroy(this.gameObject);
    }
}