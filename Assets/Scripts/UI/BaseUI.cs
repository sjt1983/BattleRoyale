using UnityEngine;

public abstract class BaseUI : MonoBehaviour
{
    public bool Initialized { get; private set; }

    public virtual void Initialize()
    {
        Initialized = true;
    }
}
