using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _position;

    private void Awake()
    {
        _cubeSpawner.Removed += BombCreate;
        _pool = new ObjectPool<Bomb>(
        createFunc: Create,
        actionOnGet: (bomb) => bomb.SetInitial(_position),
        actionOnRelease: (bomb) => bomb.Disable(),
        actionOnDestroy: (bomb) => Destroy(bomb),
        collectionCheck: true,
        defaultCapacity: 100, maxSize: 100);
    }

    private void BombCreate(Vector3 cubePosition)
    {
        _position = cubePosition;
        _pool.Get().Implemented += PutAwayPool;
    }

    private void PutAwayPool(Bomb Bomb)
    {
        Bomb.Implemented -= PutAwayPool;
        _pool.Release(Bomb);
    }
}
