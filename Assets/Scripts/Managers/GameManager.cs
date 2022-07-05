using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.AddressableAssets;

//Controls the state of the overall game.
public sealed class GameManager : NetworkBehaviour
{
    public static GameManager Instance { get; private set; }

    [SyncObject]
    public readonly SyncList<ConnectedPlayer> players = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (!IsServer)
            return;
    }

    [Server]
    public void SpawnItem()
    {
        GameObject gunPrefab = Addressables.LoadAssetAsync<GameObject>("Gun").WaitForCompletion();
        GameObject gunInstance = Instantiate(gunPrefab);
        
        Spawn(gunInstance, Owner);
        gunInstance.transform.position = new Vector3(1, 1, 1);
    }

    [Server]
    public void StartGame()
    {
        SpawnItem();
        for (int i = 0; i <= players.Count; i++)
        {
            players[i].StartGame();
        }
    }

    [Server]
    public void StopGame()
    {
        for (int i = 0; i <= players.Count; i++)
        {
            players[i].StopGame();
        }
    }
}
