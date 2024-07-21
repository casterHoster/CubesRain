using UnityEngine;

public class CubeDisplay : Display
{
    [SerializeField] private CubeSpawner _spawner;

    private void Awake()
    {
        _spawner.CountChanged += UpdateCount;
    }

    protected override void UpdateCount()
    {
        AllCount.text = "Всего кубов: " + _spawner._pool.CountAll.ToString();
        OnSceneCount.text = "Кубов на сцене: " + _spawner._pool.CountActive;
    }
}

