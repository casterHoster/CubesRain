using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>, ISpawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private Vector3 _position;

    private void Awake()
    {
        _cubeSpawner.Removed += BombCreate;
        _pool = new ObjectPool<Bomb>(
            createFunc: Create,
            actionOnGet: (bomb) => bomb.Initialize(_position),
            actionOnRelease: (bomb) => bomb.Disable());
    }

    private void BombCreate(Vector3 cubePosition)
    {
        _position = cubePosition;
        _pool.Get().Implemented += PutAwayPool;
        CountChanged?.Invoke();
    }

    public void PutAwayPool(Bomb Bomb)
    {
        Bomb.Implemented -= PutAwayPool;
        _pool.Release(Bomb);
        CountChanged?.Invoke();
    }
}
