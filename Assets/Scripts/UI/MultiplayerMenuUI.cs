using FishNet;
using UnityEngine;
using UnityEngine.UI;

public sealed class MultiplayerMenuUI : MonoBehaviour
{
    [SerializeField]
    private Button hostGameButton;

    [SerializeField]
    private Button connectToGameButton;

    private void Start()
    {
        hostGameButton.onClick.AddListener(() =>
        {
            InstanceFinder.ServerManager.StartConnection();
            InstanceFinder.ClientManager.StartConnection();
        });

        connectToGameButton.onClick.AddListener(() => InstanceFinder.ClientManager.StartConnection());
    }
}
