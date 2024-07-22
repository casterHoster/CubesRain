using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public abstract class Item<T> : MonoBehaviour
{
    protected Renderer Renderer;
    protected float MinTime = 2;
    protected float MaxTime = 5;

    public Action<T> Implemented;

    public abstract void Initialize();

    public abstract void Disable();

    protected abstract IEnumerator Count();
}
