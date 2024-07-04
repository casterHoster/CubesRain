using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _delay;

    private ObjectPool<Cube> _cubePool;
    private int _minXCoordinate = -20;
    private int _maxXCoordinate = 20;
    private int _minZCoordinate = -20;
    private int _maxZCoordinate = 20;
    private int _yCoordinate = 40;

    private void Awake()
    {
        _cubePool = new ObjectPool<Cube>(
        createFunc: () => Instantiate(_cubePrefab),
        actionOnGet: (cube) => cube.SetActiveOn(),
        actionOnRelease: (cube) => cube.SetDefault(),
        actionOnDestroy: (cube) => DestroyCube(cube),
        collectionCheck: true,
        defaultCapacity: 100, maxSize: 100);
    }

    private void DestroyCube(Cube cube)
    {
        Destroy(cube.gameObject);
    }

    private void Start()
    {
        StartCoroutine(CubesCreate());
    }

    private IEnumerator CubesCreate()
    {
        while (true)
        {
            _cubePool.Get();
            
            yield return new WaitForSeconds (_delay);
        }
    }

    private void PutAwayPool(Cube cube)
    {
        _cubePool.Release(cube);
    }

    private Cube Instantiate(Cube _cubePrefab)
    {
        Cube cube = Instantiate(_cubePrefab, new Vector3(Random.Range(_minXCoordinate, _maxXCoordinate), _yCoordinate, Random.Range(_minZCoordinate, _maxZCoordinate)), Quaternion.identity);
        cube.TimeIsOver += PutAwayPool;
        return cube;
    }
}
