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
        OverallQuantity.text = "����� ����: " + _spawner.GetOverallQuantity();
        OnSceneQuantity.text = "���� �� �����: " + _spawner.GetOnSceneQuantity();
    }
}

