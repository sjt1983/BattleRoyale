using FishNet.Connection;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;
using UnityEngine.AddressableAssets;
using System.Collections.Generic;

//Class to represent the connected user.
//Fishnet spawns this via the NetworkManager, one for every client that connects.
public sealed class ConnectedPlayer : NetworkBehaviour
{
    /** Self Instance **/
    public static ConnectedPlayer Instance { get; private set; }

    /** Sync vars - variables we with to be updated on the server **/

    //Username of the connected player.
    [SyncVar]
    public string username;

    //Pawn controlled by the user.
    [SyncVar]
    public Pawn controlledPawn;
    
    /** Fishnet Overrides **/
    public override void OnStartServer()
    {
        base.OnStartServer();
        GameManager.Instance.players.Add(this);
    }

    public override void OnStopServer()
    {
        base.OnStopServer();
        GameManager.Instance.players.Remove(this);
    }

    public override void OnStartClient()
    {
        base.OnStartClient();

        if (!IsOwner)
            return;

        Instance = this;
        UIManager.Instance.Initialize();
        UIManager.Instance.Show<LobbyUI>();
    }
    
    private void Update()
    {
        if (!IsOwner)
            return;
     
    }

    public void StartGame()
    {
        GameObject pawnPrefab = Addressables.LoadAssetAsync<GameObject>("Pawn").WaitForCompletion();
        GameObject pawnInstance = Instantiate(pawnPrefab);
        PlayerSpawnPoint[] spawnPoints = FindObjectsOfType<PlayerSpawnPoint>();

        int thePoint = Random.Range(0, spawnPoints.Length - 1);

                        
        Spawn(pawnInstance, Owner);
        pawnInstance.transform.position = spawnPoints[thePoint].transform.position;
        controlledPawn = pawnInstance.GetComponent<Pawn>();
        controlledPawn.controllingPlayer = this;
        ConnectedPlayerPawnSpawned(Owner);
    }

    public void StopGame()
    {
        if (controlledPawn != null && controlledPawn.IsSpawned)
        {
            controlledPawn.Despawn();
        }
    }

    [ServerRpc(RequireOwnership = false)]
    public void ServerSpawnPawn()
    {
        StartGame();
    }

    [TargetRpc]
    private void ConnectedPlayerPawnSpawned(NetworkConnection networkConnection)
    {
        UIManager.Instance.Show<GameUI>();
    }

    [TargetRpc]
    public void ConnectedPlayerPawnKilled(NetworkConnection networkConnection)
    {
        UIManager.Instance.Show<DeathUI>();
    }

}

