using UnityEngine;

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
            Debug.Log("FIRE");
            semiAutoLock = true;
        }
    }

    public override string GetQuantity()
    {
        return loadedAmmo.ToString();
    }
}
