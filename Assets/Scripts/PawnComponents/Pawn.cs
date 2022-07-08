using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

//Pawn is a class that represents the avatar which is controlled by the user.
public sealed class Pawn : NetworkBehaviour
{
    /** Local References **/
    [SerializeField]
    public PawnInput pawnInput;

    /** Synced Variables: Stuff we care to send to the server. **/
    
    //Player is the user who is connected to the server.
    [SyncVar]
    public ConnectedPlayer controllingPlayer;

    //How much health the Pawn has.
    [SyncVar]
    public float Health;

    //How much Stamina the Pawn has.
    [SyncVar]
    public float Stamina;

    //How much Stamina the Pawn has.
    [SyncVar]
    public bool SprintLock = false;

        //The item immediatley in front of the player in the world to interact with.
    public InteractableItem ItemPawnIsLookingAt;

    //Inventory Slots
    public GameObject itemSlotGameObject1;
    public UseableItem itemSlotUseable1;

    //Active item slot.
    public UseableItem activeSlot;

    public void Update()
    {
        if (!IsOwner)
            return;

        gameObject.name = "SELF";

        //Primary Use
        if (activeSlot != null && pawnInput.PrimaryUse)
        {
            activeSlot.StartUse();
        }
        else if (activeSlot != null)
        {
            activeSlot.StopUse();
        }

        //Reload
        if (activeSlot != null && pawnInput.Reloading)
        {
            activeSlot.Reload();
        }
    }

    //Equip and Item
    public void EquipItem(GameObject item)
    {
        //For now, change the owner to the player (transform not fishnet), equip the Item.
        Transform hand = gameObject.transform.Find("Camera").Find("Hand");
        item.transform.parent = hand;
        item.transform.rotation = hand.rotation;
        item.transform.position = hand.position;

        //Assign the GameObject
        itemSlotGameObject1 = item;
        //Assign the UseableItem component.
        itemSlotUseable1 = item.GetComponent<UseableItem>();
        item.GetComponent<UseableItem>().OwnerPawn = this;
        activeSlot = itemSlotUseable1;
    }

    //Deal damage to the pawn.
    public void ReceiveDamage(float amount)
    {
        //If we have yet to spawn in, just return.
        if (!IsSpawned)
            return;

        //Apply the damage. If we have "killed" the pawn, despawn it and inform the player.
        if ((Health -= amount ) <= 0.0f)
        {
            controllingPlayer.ConnectedPlayerPawnKilled(Owner);
            Despawn();
        }
    }

    //Make Pawn Interact with an item.
    public void InteractWithItem()
    {
        if (ItemPawnIsLookingAt != null)
        {
            ItemPawnIsLookingAt.Interact(this);
        }
    }
}
