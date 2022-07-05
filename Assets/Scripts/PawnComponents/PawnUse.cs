using FishNet.Object;
using UnityEngine;

public sealed class PawnUse : NetworkBehaviour
{
    private PawnInput pawnInput;

    private Pawn pawn;

    private void Awake()
    {
        pawnInput = GetComponent<PawnInput>();
        pawn = GetComponent<Pawn>();            
    }

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
