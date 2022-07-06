using FishNet.Object;

//Base Class for the "Useable" portion of an item.
public abstract class UseableItem : NetworkBehaviour
{
    //Flag for item usage in progress.
    protected bool IsUsing;

    //Start Using the item.
    public virtual void StartUse() {
        IsUsing = true;
    }

    //Stop Using the item.
    public virtual void StopUse()
    {
        IsUsing = false;
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
