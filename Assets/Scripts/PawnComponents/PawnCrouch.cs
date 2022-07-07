using FishNet.Object;
using UnityEngine;

public class PawnCrouch : NetworkBehaviour
{
    [SerializeField]
    private Pawn pawn;

    [SerializeField]
    private PawnInput pawnInput;

    [SerializeField]
    private Transform pawnCamera;

    private const float STAND_HEAD_Y = 1;
    private const float CROUCH_HEAD_Y = .5f;
    private const float CROUCH_SPEED = 2f;

    // Update is called once per frame
    void Update()
    {
        if (!IsOwner)
            return;

        float newpos = Mathf.Clamp(pawnInput.Crouching ? 
                                      pawnCamera.localPosition.y - (CROUCH_SPEED * Time.deltaTime) :
                                      pawnCamera.localPosition.y + (CROUCH_SPEED * Time.deltaTime), 
                                      CROUCH_HEAD_Y, STAND_HEAD_Y);
        pawnCamera.localPosition = new Vector3(pawnCamera.localPosition.x, newpos,  pawnCamera.localPosition.z);
        Debug.Log(newpos);
    }
}
