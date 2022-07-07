using FishNet.Object;
using UnityEngine;

public sealed class PawnLook : NetworkBehaviour
{
    [SerializeField]
    private Pawn pawn;

    [SerializeField]
    private PawnInput pawnInput;

    //Refernce for the camera attached to the pawn.
    [SerializeField]
    private Transform pawnCamera;

    [SerializeField]
    private LayerMask lookLayerMask;

    //Used to clamp the camera to prevent the users neck from doing vertical 360s.
    private float cameraVerticalRotationClamp = 85;
    private float cameraVerticalRotation = 0f;

    public override void OnStartClient()
    {
        base.OnStartClient();

        //We need to setup the cameras properly for the clients.
        GameObject.Find("Main Camera").SetActive(!IsOwner);
        pawnCamera.GetComponent<Camera>().enabled = (IsOwner);
        pawnCamera.GetComponent<AudioListener>().enabled = (IsOwner);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;

        //Rotate player.
        gameObject.transform.Rotate(Vector3.up, pawnInput.AdjustedMouseX);

        //Move camera up        
        cameraVerticalRotation -= pawnInput.AdjustedMouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -cameraVerticalRotationClamp, cameraVerticalRotationClamp);
        Vector3 targetRoation = transform.eulerAngles;
        targetRoation.x = cameraVerticalRotation;
        pawnCamera.transform.eulerAngles = targetRoation;

        checkToWhatPlayerIsLookingAt();
    }

    private void checkToWhatPlayerIsLookingAt()
    {
        if (Physics.Raycast(pawnCamera.position, pawnCamera.forward, out var hit, 3, lookLayerMask))
        {
            GameObject gameObjectPlayerIsLookingAt = hit.collider.gameObject;
            if (gameObjectPlayerIsLookingAt.layer == 6)
            {
                pawn.ItemPawnIsLookingAt = gameObjectPlayerIsLookingAt.GetComponent<InteractableItem>();
            }
        }
        else
        {
            pawn.ItemPawnIsLookingAt = null;
        }
    }
}
