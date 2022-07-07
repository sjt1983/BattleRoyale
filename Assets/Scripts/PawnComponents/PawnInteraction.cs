using FishNet.Object;
using UnityEngine;

//Useage for handling when the pawn interacts with an item.
public sealed class PawnInteraction : NetworkBehaviour
{
    [SerializeField]
    private PawnInput pawnInput;

    [SerializeField]
    private Pawn pawn;

    private void Update()
    {
        if (!IsOwner)
            return;

        if (pawnInput.Interacting && pawn.ItemPawnIsLookingAt != null)
        {
            pawn.InteractWithItem();
        }
    }
}
