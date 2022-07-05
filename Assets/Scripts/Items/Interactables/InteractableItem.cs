using FishNet.Object;
using UnityEngine;

public abstract class InteractableItem : NetworkBehaviour
{
    [SerializeField]
    public string ItemName;

    [SerializeField]
    public string InteractText;

    public abstract void Interact(Pawn pawn);

    public string GetInteractText()
    {
        return InteractText + " " + ItemName;
    }
}
