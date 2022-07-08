using UnityEngine;
using FishNet.Object;
using FishNet.Object.Synchronizing;

//Pawn is a class that represents the avatar which is controlled by the user.
public sealed class Pawn : NetworkBehaviour
{
    /** Local References **/
    [SerializeField]
    public PawnInput pawnInput;

    /** Synced Variables: Stuff we care to send to the server. **/
    
    //Player is the user who is connected to the server.
    [SyncVar]
    public ConnectedPlayer controllingPlayer;

    //How much health the Pawn has.
    [SyncVar]
    public float Health;

    //How much Stamina the Pawn has.
    [SyncVar]
    public float Stamina;

    //If the player sprinted too much and must recover stamina
    [SyncVar]
    public bool SprintLock = false;        

    [SerializeField]
    private float recoil = 0f;

    public float ZoomFov = 0f;
    public float DefaultFov;
    Transform hand;

    //The item immediatley in front of the player in the world to interact with.
    public InteractableItem ItemPawnIsLookingAt;

    //Inventory Slots
    public GameObject itemSlotGameObject1;
    public UseableItem itemSlotUseable1;

    //Active item slot.
    public UseableItem activeSlot;

    public bool Initialized = false;

    public Vector3 targetHandPosition;

    private void Initialize()
    {
        //Name this one "SELF" so we can ignore it for raycasts for bullets
        gameObject.name = "SELF";
        DefaultFov = gameObject.GetComponentInChildren<Camera>().fieldOfView;

        hand = transform.Find("Camera").Find("Hand").transform;
        Initialized = true;
    }

    public void Update()
    {
        if (!IsOwner)
            return;

        if (!Initialized)
            Initialize();

        //Primary Use
        if (activeSlot != null && pawnInput.PrimaryUse)
        {
            activeSlot.StartPrimaryUse();
        }
        else if (activeSlot != null)
        {
            activeSlot.StopPrimaryUse();
        }

        //Secondary Use
        if (activeSlot != null && pawnInput.SecondaryUse)
        {
            activeSlot.StartSecondaryUse();
            
        }
        else if (activeSlot != null)
        {
            activeSlot.StopSecondaryUse();
        }

        if (targetHandPosition != null)
            hand.transform.localPosition = targetHandPosition;


        //Reload
        if (activeSlot != null && pawnInput.Reloading)
        {
            activeSlot.Reload();
        }
    }

    //Equip an Item
    public void EquipItem(GameObject item)
    {
        //For now, change the owner to the player (transform not fishnet), equip the Item.
        Transform hand = gameObject.transform.Find("Camera").Find("Hand");
        item.transform.parent = hand;
        item.transform.rotation = hand.rotation;
        item.transform.position = hand.position;

        //Assign the GameObject
        itemSlotGameObject1 = item;
        //Assign the UseableItem component.
        itemSlotUseable1 = item.GetComponent<UseableItem>();
        item.GetComponent<UseableItem>().OwnerPawn = this;
        activeSlot = itemSlotUseable1;
    }

    //Deal damage to the pawn.
    public void ReceiveDamage(float amount)
    {
        //If we have yet to spawn in, just return.
        if (!IsSpawned)
            return;

        //Apply the damage. If we have "killed" the pawn, despawn it and inform the player.
        if ((Health -= amount ) <= 0.0f)
        {
            controllingPlayer.ConnectedPlayerPawnKilled(Owner);
            Despawn();
        }
    }

    //Make Pawn Interact with an item.
    public void InteractWithItem()
    {
        if (ItemPawnIsLookingAt != null)
        {
            ItemPawnIsLookingAt.Interact(this);
        }
    }

    //Add recoil to the player
    //Recoil bounces the camera vertically and drifts horizontally while there is recoil
    public void AddRecoil(float intensity)
    {
        if (recoil < 0)
            recoil = 0;

        recoil += intensity;
    }

    //Recoil is achieved by adding an intensity to the Pawn
    //For now, we deduct the intensity and adjust the camera vertically by 20 degrees a second
    //To allow a drop after the recoil, we dip negative 1.5 degrees after all firing has stopped.
    public float GetEffectiveRecoil()
    {
        float retval = 20 * Time.deltaTime;
        recoil -= retval;

        if (recoil < 0)
            retval *= -1;

        if (recoil <= -1.5f)
            return 0;

        return retval;
    }
}
