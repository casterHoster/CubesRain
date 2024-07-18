using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class Display<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] protected Spawner<T> _spawner;
    [SerializeField] protected TextMeshProUGUI _allCount;
    [SerializeField] protected TextMeshProUGUI _onSceneCount;

    private void Awake()
    {
        _spawner.CountChanged += UpdateCount;
    }

    protected abstract void UpdateCount();
    
}
