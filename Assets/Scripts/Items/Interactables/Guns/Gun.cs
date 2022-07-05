using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object;

public class Gun : InteractableItem
{
    [ServerRpc (RequireOwnership = false)]
    public override void Interact(Pawn pawn)
    {
        Despawn();
    }

}
