using FishNet.Object;

//Useage for handling when the pawn interacts with an item.
public sealed class PawnInteraction : NetworkBehaviour
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
