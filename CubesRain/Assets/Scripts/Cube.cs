using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(BoxCollider))]


public class Cube : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;

    private Renderer _renderer;
    private bool _isTouched;
    private float _minTime = 2;
    private float _maxTime = 5;

    public event UnityAction<Cube> TimeIsOver;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void SetActiveOn()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Platform platform) && _isTouched == false)
        {
            _isTouched = true;
            SetRandomMaterial();
            StartCoroutine(StartTimerToDestroy(SetTime()));
        }
    }

    private void SetRandomMaterial()
    {
        _renderer.sharedMaterial = _materials[Random.Range(0, _materials.Count)];
    }

    private float SetTime()
    {
        return Random.Range(_minTime, _maxTime + 1);
    }

    private IEnumerator StartTimerToDestroy(float time)
    {
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }

        TimeIsOver?.Invoke(this);
    }

    public void SetDefault() 
    { 
        _renderer.sharedMaterial = default;
        gameObject.SetActive(false);
    }
}
