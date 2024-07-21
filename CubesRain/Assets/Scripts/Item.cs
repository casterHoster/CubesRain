using System.Collections;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    protected Renderer Renderer;
    protected float MinTime = 2;
    protected float MaxTime = 5;

    public abstract void Initialize();

    public abstract void Initialize(Vector3 vector3);

    public abstract void Disable();

    protected abstract IEnumerator Count();
}
