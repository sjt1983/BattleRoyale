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
    public void SpawnItems()
    {
        LootSpawnPoint[] lootSpawnPoints = FindObjectsOfType<LootSpawnPoint>();
        for (int x = 0; x < lootSpawnPoints.Length; x++)
        {
            lootSpawnPoints[x].SpawnItems();
        }
    }

    [Server]
    public void StartGame()
    {
        SpawnItems();
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
