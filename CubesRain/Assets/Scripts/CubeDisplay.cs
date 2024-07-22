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
        OverallQuantity.text = "Всего кубов: " + _spawner.GetOverallQuantity();
        OnSceneQuantity.text = "Кубов на сцене: " + _spawner.GetOnSceneQuantity();
    }
}

