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

    private static readonly float ZOOM_SPEED = 100;

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
                
        //Determine Recoil values for camera movement/player rotation
        float effectiveRecoil = pawn.GetEffectiveRecoil();
        float recoilRotation = pawn.GetEffectiveRecoil() == 0f ? 0f : 2f * (Random.Range(1,3) == 1 ? 1 : -1) * Time.deltaTime;
        
        //Rotate player.
        gameObject.transform.Rotate(Vector3.up, pawnInput.AdjustedMouseX + recoilRotation);

        //Move camera up
        cameraVerticalRotation -= pawnInput.AdjustedMouseY + effectiveRecoil;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -cameraVerticalRotationClamp, cameraVerticalRotationClamp);
        Vector3 targetRoation = transform.eulerAngles;
        targetRoation.x = cameraVerticalRotation;
        pawnCamera.transform.eulerAngles = targetRoation;

        checkToWhatPlayerIsLookingAt();

        //Adjust the FOV based on zooming.
        float currentFov = pawnCamera.GetComponent<Camera>().fieldOfView;
        //ZOOMING IN - removing from FOV
        if (currentFov > pawn.DefaultFov - pawn.ZoomFov)
        {
            currentFov -= ZOOM_SPEED * Time.deltaTime;
            currentFov = Mathf.Clamp(currentFov, pawn.DefaultFov - pawn.ZoomFov, 90);
        }
        //ZOOMING OUT - adding to FOV
        else if (currentFov < pawn.DefaultFov - pawn.ZoomFov)
        {
            currentFov += ZOOM_SPEED * Time.deltaTime;
            currentFov = Mathf.Clamp(currentFov, 0, pawn.DefaultFov);
        }

        pawnCamera.GetComponent<Camera>().fieldOfView = currentFov;
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
