using UnityEngine;
using UnityEngine.AddressableAssets;

public abstract class GunBase : UseableItem
{
    protected GameObject bulletPrefab;

    private void Awake()
    {
        bulletPrefab = Addressables.LoadAssetAsync<GameObject>("Bullet").WaitForCompletion();
    }

    protected GameObject FireBullet(float bloom)
    {
        Transform position = OwnerPawn.transform.Find("Camera");
        float bloomUp = Random.Range(bloom, -bloom);
        float bloomRight = Random.Range(bloom, -bloom);

        RaycastHit[] hits = Physics.RaycastAll(position.position, position.forward + (position.right * bloomRight) + position.up * bloomUp);
        Vector3 hitPoint;
        GameObject hit = GetObjectsBulletHit(hits, out hitPoint);

        Debug.DrawRay(position.position, (position.forward + (position.right * bloomRight) + position.up * bloomUp) * 1000, Color.green, 10000f);

        if (hit != null)
        {
            GameObject bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.position = hitPoint;
            Spawn(bulletInstance, Owner);

            bulletInstance = Instantiate(bulletPrefab);
            bulletInstance.transform.position = hitPoint;
            Spawn(bulletInstance, Owner);
        }
        return hit;
    }

    protected GameObject GetObjectsBulletHit(RaycastHit[] raycastHits, out Vector3 returnHitPoint)
    {
        GameObject hitGameObject = null;
        returnHitPoint = new Vector3();

        //Start with an impossibly long distance.
        float distance = 999999999.0f;
        foreach (RaycastHit hit in raycastHits)
        {
            //Ignore the "SELF" pawn.
            if (hit.transform.name == "SELF")
                continue;

            //If the current hit in the list of hits is closer than the current closest hit, then that is our potential target.
            if (hit.distance < distance)
            {
                distance = hit.distance;
                hitGameObject = hit.transform.gameObject;
                returnHitPoint = hit.point;
            }
        }
        return hitGameObject;
    }

    protected void RecoilCamera(float intensity)
    {
        OwnerPawn.AddRecoil(intensity);
    }
}
