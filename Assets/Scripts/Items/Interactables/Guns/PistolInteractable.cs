using FishNet.Object;

//Pistol Interactable, used to equip
public class PistolInteractable : InteractableItem
{
    [ServerRpc (RequireOwnership = false)]
    public override void Interact(Pawn pawn)
    {
        pawn.EquipItem(gameObject);
    }

}
