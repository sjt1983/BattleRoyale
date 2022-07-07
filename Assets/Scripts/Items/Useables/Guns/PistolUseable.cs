using UnityEngine;
using UnityEngine.AddressableAssets;

//Script for the Pistol gun.
public class PistolUseable : UseableItem
{
    //How much ammo is in the gun.
    private int loadedAmmo = 8;

    //The pistol is semi-auto, so the player must click once for each bullet.
    private bool semiAutoLock = false;

    public override void StartUse()
    {
        base.StartUse();
    }

    public override void StopUse()
    {
        base.StopUse();
        semiAutoLock = false;
    }


    public override string GetItemName()
    {
        return "Pistol";
    }

    public override void Reload()
    {
        loadedAmmo = 8;

    }

    // Update is called once per frame
    void Update()
    {
        //If the player has hit the use key, and the gun did not shoot already, and we have ammo, fire!
        if (IsUsing && !semiAutoLock && loadedAmmo > 0)
        {
            loadedAmmo--;
            Transform pawnCamera = OwnerPawn.transform.Find("Camera");
            if (Physics.Raycast(pawnCamera.position + (pawnCamera.forward * 1), pawnCamera.forward, out var hit))
            {
                GameObject bulletPrefab = Addressables.LoadAssetAsync<GameObject>("Bullet").WaitForCompletion();
                GameObject bulletInstance = Instantiate(bulletPrefab);
                Debug.Log(hit.transform.name);
                Spawn(bulletInstance, Owner);
                bulletInstance.transform.position = hit.point;
            }
            semiAutoLock = true;
        }
    }

    public override string GetQuantity()
    {
        return loadedAmmo.ToString();
    }
}
