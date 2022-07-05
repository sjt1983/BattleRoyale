using FishNet.Object;
using UnityEngine;

public sealed class PawnWeapon : NetworkBehaviour
{
    private Pawn pawn;

    private PawnInput pawnInput;

    private float damage;

    private float shotDelay;

    private float timeUntilNextShot;

    [SerializeField]
    private Transform firePoint;

    public override void OnStartNetwork()
    {
        base.OnStartNetwork();

        pawn = GetComponent<Pawn>();
        pawnInput = GetComponent<PawnInput>();
    }

    private void Update()
    {
        if (!IsOwner)
            return;

        if (timeUntilNextShot <= 0.0f)
        {
            if (pawnInput.fire)
            {
                ServerFire(firePoint.position, firePoint.forward);
                timeUntilNextShot = 1f;
            }            
        }
        else
        {
            timeUntilNextShot -= Time.deltaTime;
        }
    }

    [ServerRpc]
    private void ServerFire(Vector3 firePointPosition, Vector3 firePointDirection)
    {
        if (Physics.Raycast(firePointPosition, firePointDirection, out RaycastHit hit) && hit.transform.TryGetComponent(out Pawn pawn))
        {
            pawn.ReceiveDamage(25);
        }
    }
}
