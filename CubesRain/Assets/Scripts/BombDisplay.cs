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
        OverallQuantity.text = "Всего бомб: " + _spawner.GetOverallQuantity();
        OnSceneQuantity.text = "Бомб на сцене: " + _spawner.GetOnSceneQuantity();
    }
}

