using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class CubeSpawner : Spawner<Cube>, ISpawner<Cube>
{
    [SerializeField] private float _delay;

    private WaitForSeconds _waiting;

    public event UnityAction<Vector3> Removed;

    private void OnEnable()
    {
        _waiting = new WaitForSeconds(_delay);
    }

    private void Start()
    {
        StartCoroutine(CubesCreate());
    }

    private IEnumerator CubesCreate()
    {
        while (enabled)
        {
            Pool.Get().Implemented += PutAwayPool;
            CountChanged?.Invoke();
            yield return _waiting;
        }
    }

    public override void PutAwayPool(Cube cube)
    {
        base.PutAwayPool(cube);
        Removed?.Invoke(cube.transform.position);
    }
}
