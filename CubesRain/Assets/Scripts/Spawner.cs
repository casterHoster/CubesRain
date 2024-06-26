using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private float _delay;

    private int _minXCoordinate = -20;
    private int _maxXCoordinate = 20;
    private int _minZCoordinate = -20;
    private int _maxZCoordinate = 20;
    private int _yCoordinate = 40;

    private void Start()
    {
        StartCoroutine(CubesCreate());
    }

    private IEnumerator CubesCreate()
    {
        while (true)
        {
            Cube newCube = Instantiate(_cubePrefab, new Vector3(Random.Range(_minXCoordinate, _maxXCoordinate), _yCoordinate, Random.Range(_minZCoordinate, _maxZCoordinate)), Quaternion.identity);
            yield return new WaitForSeconds (_delay);
        }

    }
}
