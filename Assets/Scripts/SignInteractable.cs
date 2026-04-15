using UnityEngine;

public class SignInteractable : Interactable
{
    [SerializeField]
    Sprite signBackground;

    [SerializeField]
    string signText;

    public override void Interact()
    {
        GameManager.Instance.ShowInfoOnScreen(signBackground, signText);
    }
}
