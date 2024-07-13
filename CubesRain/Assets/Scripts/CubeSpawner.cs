using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _delay;

    private WaitForSeconds _waiting;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_delay);
        _pool = new ObjectPool<Cube>(
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
            _pool.Get().IsTimeOver += PutAwayPool;

            yield return _waiting;
        }
    }

    private void PutAwayPool(Cube cube)
    {
        cube.IsTimeOver -= PutAwayPool;
        _pool.Release(cube);
    }
}
