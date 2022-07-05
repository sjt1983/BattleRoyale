using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

//Pawn is a class that represents the avatar which is controlled by the user.
public sealed class Pawn : NetworkBehaviour
{
    /** Synced Variables: Stuff we care to send to the server. **/
    
    //Player is the user who is connected to the server.
    [SyncVar]
    public ConnectedPlayer controllingPlayer;

    //How much health the Pawn has.
    [SyncVar]
    public float Health;

    public InteractableItem ItemPawnIsLookingAt;

    public void Update()
    {
        if (!IsOwner)
            return;

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
