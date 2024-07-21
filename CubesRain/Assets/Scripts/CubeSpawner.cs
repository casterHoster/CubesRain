using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>, ISpawner<Cube>
{
    [SerializeField] private float _delay;

    private WaitForSeconds _waiting;

    public event UnityAction<Vector3> Removed;

    private void Awake()
    {
        _waiting = new WaitForSeconds(_delay);
        _pool = new ObjectPool<Cube>(
            createFunc: Create,
            actionOnGet: (cube) => cube.Initialize(),
            actionOnRelease: (cube) => cube.Disable()
        );
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
            CountChanged?.Invoke();
            yield return _waiting;
        }
    }

    public void PutAwayPool(Cube cube)
    {
        cube.IsTimeOver -= PutAwayPool;
        Removed?.Invoke(cube.transform.position);
        _pool.Release(cube);
        CountChanged?.Invoke();
    }
}
