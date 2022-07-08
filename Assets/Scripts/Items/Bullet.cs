using FishNet.Object;
using UnityEngine;
public class Bullet : NetworkBehaviour
{

    float time = 20;

    private void Update()
    {
        time -= Time.deltaTime;

        if (time <= 0f)
        {
            Despawn();
        }
    }

}
