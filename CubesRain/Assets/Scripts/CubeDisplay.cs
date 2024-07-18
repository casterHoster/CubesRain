using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeDisplay : Display<CubeSpawner>
{
    protected override void UpdateCount()
    {
        _allCount.text = "����� �����: " + _spawner._pool.CountAll.ToString();
        _onSceneCount.text = "����� �� �����: " + _spawner._pool.CountActive;
    }
}

