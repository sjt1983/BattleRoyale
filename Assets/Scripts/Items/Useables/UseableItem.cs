using FishNet.Object;

//Base Class for the "Useable" portion of an item.
public abstract class UseableItem : NetworkBehaviour
{
    //The pawn which owns the object.
    public Pawn OwnerPawn;

    //Flag for item primary usage in progress.
    protected bool IsPrimaryUsingItem;

    //Flag for item primary usage in progress.
    protected bool IsSecondaryUsingItem;

    //Start Primary Using the item.
    public virtual void StartPrimaryUse() {
        IsPrimaryUsingItem = true;
    }

    //Stop Primary Using the item.
    public virtual void StopPrimaryUse()
    {
        IsPrimaryUsingItem = false;
    }

    //Start Secondary Using the item.
    public virtual void StartSecondaryUse()
    {
        IsSecondaryUsingItem = true;
    }

    //Stop Secondary Using the item.
    public virtual void StopSecondaryUse()
    {
        IsSecondaryUsingItem = false;
    }

    //Reload for guns.
    public virtual void Reload()
    {
        //Default implementation is do nothing;
    }

    //Used by the UI to display the item name
    public abstract string GetItemName();

    //Used by the UI to display the item quantity.
    public abstract string GetQuantity();
}
