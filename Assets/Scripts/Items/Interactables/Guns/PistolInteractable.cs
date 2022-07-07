using FishNet.Object;
using UnityEngine;
//Pistol Interactable, used to equip the pistol
public class PistolInteractable : InteractableItem
{
    public override void DisableInteractions()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = false;
    }

    public override void EnableInteractions()
    {
        gameObject.GetComponent<CapsuleCollider>().enabled = true;
    }

    [ServerRpc (RequireOwnership = false)]
    public override void Interact(Pawn pawn)
    {
        DisableInteractions();
        pawn.EquipItem(gameObject);
    }
}
