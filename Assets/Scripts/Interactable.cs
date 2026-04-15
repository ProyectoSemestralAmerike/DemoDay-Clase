/*
 * Code by: Chris D. Alvarado
 * Created on: November 2025
 * Last Update: November 25th 2025
 * 
 * */

using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField]
    string name;
    public string Name
    {
        get
        {
            return name;
        }
    }

    [SerializeField]
    string interactLabel = "Interact";
    public string Label
    {
        get
        {
            return interactLabel;
        }
    }

    [SerializeField]
    string tag = "InteractableTag";
    public string Tag
    {
        get
        {
            return tag;
        }
    }

    [SerializeField]
    protected InteractableState state = InteractableState.Detectable;
    public InteractableState State
    {
        get
        {
            return state;
        }
    }

    [SerializeField]
    protected bool hidesOnFirstInteraction = true;
    public bool HidesOnFirstInteraction
    {
        get
        {
            return hidesOnFirstInteraction;
        }
    }


    public virtual void Interact() { }

    protected virtual void EndInteraction() { }

    public virtual string GetInteractPrompt() { return Label; }
}


public enum InteractableState
{
    Detectable,
    Locked,
    Hidden
}
