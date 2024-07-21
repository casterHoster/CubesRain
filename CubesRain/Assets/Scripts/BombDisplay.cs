using UnityEngine;

public class BombDisplay : Display
{
    [SerializeField] private BombSpawner _spawner;

    private void Awake()
    {
        _spawner.CountChanged += UpdateCount;
    }

    protected override void UpdateCount()
    {
        AllCount.text = "Всего бомб: " + _spawner._pool.CountAll.ToString();
        OnSceneCount.text = "Бомб на сцене: " + _spawner._pool.CountActive;
    }
}

