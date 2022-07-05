using FishNet;
using UnityEngine;

public sealed class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField]
    private BaseUI[] views;

    private void Awake()
    {
        Instance = this;
        Initialize();
    }

    public void Initialize()
    {
        foreach (BaseUI view in views)
        {
            view.Initialize();
        }

        if (InstanceFinder.IsServer)
        {
            Show<LobbyUI>();
        }
    }

    public void Show<T>() where T: BaseUI
    {
        foreach(BaseUI view in views)
        {
            view.gameObject.SetActive(view is T);
        }
    }
}
