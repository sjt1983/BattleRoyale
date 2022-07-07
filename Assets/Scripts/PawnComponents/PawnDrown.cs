using FishNet.Object;
using UnityEngine;

public class PawnDrown : NetworkBehaviour
{
    [SerializeField]
    Pawn pawn;

    private float drownDamagePerSecond = 2.0f;
    private float waterLevelFromGround = 1.5f;

    void Update()
    {
        if (!IsOwner)
            return;

        if (pawn.gameObject.transform.position.y < waterLevelFromGround)
        {
            pawn.ReceiveDamage(drownDamagePerSecond * Time.deltaTime);
        }
    }
}
