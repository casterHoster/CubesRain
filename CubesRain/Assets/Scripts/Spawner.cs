using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Item<T>
{
    [SerializeField] private T _prefab;

    protected ObjectPool<T> Pool;
    private int _minXCoordinate = -20;
    private int _maxXCoordinate = 20;
    private int _minZCoordinate = -20;
    private int _maxZCoordinate = 20;
    private int _yCoordinate = 40;

    public Action CountChanged;

    protected virtual void Awake()
    {
        Pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnGet: (item) => item.Initialize(),
            actionOnRelease: (item) => item.Disable());
    }

    public virtual void PutAwayPool(T item)
    {
        item.Implemented -= PutAwayPool;
        Pool.Release(item);
        CountChanged?.Invoke();
    }

    public int GetOverallQuantity()
    {
        return Pool.CountAll;
    }

    public int GetOnSceneQuantity()
    {
        return Pool.CountActive;
    }

    protected T Create()
    {
        T obj = Instantiate(
            _prefab,
            GetRandomPosition(),
            Quaternion.identity);
        return obj;
    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(UnityEngine.Random.Range(_minXCoordinate, _maxXCoordinate),
            _yCoordinate, UnityEngine.Random.Range(_minZCoordinate, _maxZCoordinate));
    }
}
