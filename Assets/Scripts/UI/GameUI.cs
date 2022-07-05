using TMPro;
using UnityEngine;

public class GameUI : BaseUI
{
    [SerializeField]
    private TextMeshProUGUI healthText;

    [SerializeField]
    private TextMeshProUGUI pickupText;

    public void Update()
    {
        if (!Initialized)
            return;

        ConnectedPlayer player = ConnectedPlayer.Instance;

        if (player == null || player.controlledPawn == null)
            return;

        healthText.text = $"Health: {player.controlledPawn.Health}";
        
        if (player.controlledPawn.ItemPawnIsLookingAt != null)
        {
            pickupText.text = $"{player.controlledPawn.ItemPawnIsLookingAt.GetInteractText()}";
        }
        else
        {
            pickupText.text = "";
        }
    }
}
