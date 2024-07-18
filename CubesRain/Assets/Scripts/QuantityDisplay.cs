using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuantityDisplay<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] Spawner<T> _spawner;
    [SerializeField] private TextMeshProUGUI _allCount;
    [SerializeField] private TextMeshProUGUI _onSceneCount;
    [SerializeField] private string _countAllObjects;
    [SerializeField] private string _countOnSceneObjects;

    private void Awake()
    {
        
    }

    private void UpdateCubesCount()
    {
        _allCount.text = _countAllObjects + _spawner._pool.CountAll.ToString();
        _onSceneCount.text = _countOnSceneObjects + _spawner._pool.CountActive;
    }
}
