using FishNet;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUI : BaseUI
{
    [SerializeField]
    private Button startGameButton;

    public override void Initialize()
    {
        if (InstanceFinder.IsServer)
        {
            startGameButton.onClick.AddListener(() => GameManager.Instance.StartGame());
            startGameButton.gameObject.SetActive(true);
        }
        else
        {
            startGameButton.gameObject.SetActive(false);
        }
        base.Initialize();
    }

    private void Update()
    {
        if (!Initialized)
            return;
    }
}
