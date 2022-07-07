using FishNet.Object;
using UnityEngine;

//Base class for items that exist in the world that players interact with.
public abstract class InteractableItem : NetworkBehaviour
{
    //The name of the item the Pawn is looking at.
    [SerializeField]
    public string ItemName;

    //The Description of the action for the item.
    //e.g. for "Pickup" Pistol or "Open" Door.
    [SerializeField]
    public string InteractText;

    //Script entrypoint to determine what to do with the item.
    public abstract void Interact(Pawn pawn);

    public string GetInteractText()
    {
        return InteractText + " " + ItemName;
    }
}
