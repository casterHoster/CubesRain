using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDisplay : Display<CubeSpawner>
{
    protected override void UpdateCount()
    {
        _allCount.text = "Всего кубов: " + _spawner._pool.CountAll.ToString();
        _onSceneCount.text = "Кубов на сцене: " + _spawner._pool.CountActive;
    }
}

