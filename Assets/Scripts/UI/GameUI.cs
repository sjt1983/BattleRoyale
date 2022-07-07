using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private TextMeshProUGUI staminaText;

    [SerializeField]
    private TextMeshProUGUI pickupText;

    [SerializeField]
    private TextMeshProUGUI primarySlotName;

    [SerializeField]
    private TextMeshProUGUI primarySlotQuantity;

    public void Update()
    {
        if (!Initialized)
            return;

        ConnectedPlayer player2 = ConnectedPlayer.Instance;
        if (player2 == null)
            return;

        Pawn pawn = player2.controlledPawn;
        if (pawn == null)
            return;

        //Health Bar.
        healthText.text = $"Health: {(int)Mathf.Ceil(pawn.Health)}";

        //Stamina
        staminaText.color = pawn.SprintLock ? Color.red : Color.white;
        staminaText.text = $"Stamina: {(int)Mathf.Ceil(pawn.Stamina)}";

        //Interact Notification
        if (pawn.ItemPawnIsLookingAt != null)
        {
            pickupText.text = $"{pawn.ItemPawnIsLookingAt.GetInteractText()}";
        }
        else
        {
            pickupText.text = "";
        }

        //Primary Item
        if (pawn.itemSlotUseable1 != null)
        {
            primarySlotName.text = pawn.itemSlotUseable1.GetItemName();
            primarySlotQuantity.text = pawn.itemSlotUseable1.GetQuantity();
        }
        else
        {
            primarySlotName.text = "";
            primarySlotQuantity.text = "";
        }
    }
}
