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
        AllCount.text = "����� �����: " + _spawner._pool.CountAll.ToString();
        OnSceneCount.text = "����� �� �����: " + _spawner._pool.CountActive;
    }
}

