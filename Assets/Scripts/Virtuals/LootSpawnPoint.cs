using FishNet.Object;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LootSpawnPoint : NetworkBehaviour

{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(gameObject.transform.position, new Vector3(1, 1, 1));
    }

    [ServerRpc (RequireOwnership =false)]
    public void SpawnItems()
    {
        GameObject gunPrefab = Addressables.LoadAssetAsync<GameObject>("Pistol").WaitForCompletion();
        GameObject gunInstance = Instantiate(gunPrefab);

        Spawn(gunInstance, Owner);
        gunInstance.transform.position = transform.position;
    }
}
