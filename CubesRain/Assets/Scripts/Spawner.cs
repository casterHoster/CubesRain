using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;

    public ObjectPool<T> _pool;
    private int _minXCoordinate = -20;
    private int _maxXCoordinate = 20;
    private int _minZCoordinate = -20;
    private int _maxZCoordinate = 20;
    private int _yCoordinate = 40;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: Create,
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: 1000, maxSize: 1000);
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
        return new Vector3(Random.Range(_minXCoordinate, _maxXCoordinate),
            _yCoordinate, Random.Range(_minZCoordinate, _maxZCoordinate));
    }
}
