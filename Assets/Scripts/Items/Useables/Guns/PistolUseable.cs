using UnityEngine;
//Script for the Pistol gun.
public class PistolUseable : GunBase
{
    //How much ammo is in the gun.
    private int loadedAmmo = 8;

    //How much bloom the gun has by default.
    private float bloom = .01f;

    //How much recoil is added due to bloom.
    [SerializeField]
    private float recoilBloom = 0f;

    //How much recoil bloom to add per shot.
    private float recoilBloomPerShot = .003f;

    //How fast the bloom recovers per second
    private float recoilBloomRecoveryPerSecond = .02f;

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
            FireBullet(bloom + recoilBloom);
            recoilBloom += recoilBloomPerShot;
            semiAutoLock = true;
        }

        //Recoil bloom cant go below zero and maxes at 1, for now, this is large and will never happen but for now I dont want to limit it.
        recoilBloom = Mathf.Clamp(recoilBloom - recoilBloomRecoveryPerSecond * Time.deltaTime, 0, 1);
    }

    public override string GetQuantity()
    {
        return loadedAmmo.ToString();
    }
}
