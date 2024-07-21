using System;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : Item
{
    [SerializeField] private T _prefab;

    public ObjectPool<T> _pool;
    private int _minXCoordinate = -20;
    private int _maxXCoordinate = 20;
    private int _minZCoordinate = -20;
    private int _maxZCoordinate = 20;
    private int _yCoordinate = 40;

    public Action CountChanged;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 1000, maxSize: 1000);
            //actionOnRelease: (obj) => obj.Disable());
    }

    protected T Create()
    {
        T obj = Instantiate(
            _prefab,
            GetVector3(),
            Quaternion.identity);
        return obj;
    }

    private Vector3 GetVector3()
    {
        return new Vector3(UnityEngine.Random.Range(_minXCoordinate, _maxXCoordinate),
            _yCoordinate, UnityEngine.Random.Range(_minZCoordinate, _maxZCoordinate));
    }
}
