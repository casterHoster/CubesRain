using UnityEngine;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>, ISpawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;

    private void OnEnable()
    {
        _cubeSpawner.Removed += BombCreate;
    }

    private void BombCreate(Vector3 cubePosition)
    {
        Bomb bomb = Pool.Get();
        bomb.Implemented += PutAwayPool;
        bomb.transform.position = cubePosition;
        CountChanged?.Invoke();
    }
}
