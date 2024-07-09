using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T _prefab;
    //[SerializeField] private float _delay;

    private ObjectPool<T> _cubePool;
    private int _minXCoordinate = -20;
    private int _maxXCoordinate = 20;
    private int _minZCoordinate = -20;
    private int _maxZCoordinate = 20;
    private int _yCoordinate = 40;
    //private WaitForSeconds _waiting;

    private void Awake()
    {
        //_waiting = new WaitForSeconds(_delay);
        _cubePool = new ObjectPool<T>(
        createFunc: Create,
        //actionOnGet: (cube) => cube.SetInitial(),
        //actionOnRelease: (cube) => cube.Disable(),
        actionOnDestroy: (cube) => Object.Destroy(cube),
        collectionCheck: true,
        defaultCapacity: 100, maxSize: 100);
    }

    //private void Start()
    //{
    //    StartCoroutine(CubesCreate());
    //}

    //private IEnumerator CubesCreate()
    //{
    //    while (enabled)
    //    {
    //        _cubePool.Get().IsTimeOver += PutAwayPool;

    //        yield return _waiting;
    //    }
    //}

    //private void PutAwayPool(T obj)
    //{
    //    //obj.IsTimeOver -= PutAwayPool;
    //    _cubePool.Release(obj);
    //}

    private T Create()
    {
        T obj = Object.Instantiate(
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
