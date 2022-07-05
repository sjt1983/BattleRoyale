using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUI : BaseUI
{
    [SerializeField]
    private Button respawnButton;
    public override void Initialize()
    {
        respawnButton.onClick.AddListener(() =>
        {
            ConnectedPlayer.Instance.ServerSpawnPawn();
        });

        base.Initialize();
    }
}

