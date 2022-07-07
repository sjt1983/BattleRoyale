using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    [SerializeField]
    private TextMeshProUGUI healthText;

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

        ConnectedPlayer player = ConnectedPlayer.Instance;

        if (player == null || player.controlledPawn == null)
            return;

        //Health Bar.
        healthText.text = $"Health: {player.controlledPawn.Health}";
        
        //Interact Notification
        if (player.controlledPawn.ItemPawnIsLookingAt != null)
        {
            pickupText.text = $"{player.controlledPawn.ItemPawnIsLookingAt.GetInteractText()}";
        }
        else
        {
            pickupText.text = "";
        }

        //Primary Item
        if (player.controlledPawn.itemSlotUseable1 != null)
        {
            primarySlotName.text = player.controlledPawn.itemSlotUseable1.GetItemName();
            primarySlotQuantity.text = player.controlledPawn.itemSlotUseable1.GetQuantity();
        }
        else
        {
            primarySlotName.text = "";
            primarySlotQuantity.text = "";
        }
    }
}
