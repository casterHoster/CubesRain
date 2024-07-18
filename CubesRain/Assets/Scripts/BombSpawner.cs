using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class BombSpawner : Spawner<Bomb>
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private TextMeshProUGUI _allCount;
    [SerializeField] private TextMeshProUGUI _onSceneCount;

    private Vector3 _position;

    private void Awake()
    {
        _cubeSpawner.Removed += BombCreate;
        _pool = new ObjectPool<Bomb>(
            createFunc: Create,
            actionOnGet: (bomb) => bomb.SetInitial(_position),
            actionOnRelease: (bomb) => bomb.Disable());
    }

    private void Update()
    {
        _allCount.text = "Всего бомб: " + _pool.CountAll.ToString();
        _onSceneCount.text = "Бомб на сцене: " + _pool.CountActive;
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
