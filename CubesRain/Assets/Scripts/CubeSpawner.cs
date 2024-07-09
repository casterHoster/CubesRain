using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _delay;

    private ObjectPool<Cube> _cubePool;
    private int _minXCoordinate = -20;
    private int _maxXCoordinate = 20;
    private int _minZCoordinate = -20;
    private int _maxZCoordinate = 20;
    private int _yCoordinate = 40;
    private WaitForSeconds _waiting;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_delay);
        _cubePool = new ObjectPool<Cube>(
        createFunc: Create,
        actionOnGet: (cube) => cube.SetInitial(),
        actionOnRelease: (cube) => cube.Disable(),
        actionOnDestroy: (cube) => Destroy(cube),
        collectionCheck: true,
        defaultCapacity: 100, maxSize: 100);
    }

    private void Start()
    {
        StartCoroutine(CubesCreate());
    }

    private IEnumerator CubesCreate()
    {
        while (enabled)
        {
            _cubePool.Get().IsTimeOver += PutAwayPool;

            yield return _waiting;
        }
    }

    private void PutAwayPool(Cube cube)
    {
        cube.IsTimeOver -= PutAwayPool;
        _cubePool.Release(cube);
    }

    private Cube Create()
    {
        Cube cube = Instantiate(
            _cubePrefab,
            new Vector3(Random.Range(_minXCoordinate, _maxXCoordinate),
            _yCoordinate, Random.Range(_minZCoordinate, _maxZCoordinate)),
            Quaternion.identity);
        return cube;
    }
}
