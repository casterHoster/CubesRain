using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(BoxCollider))]
public class Cube : MonoBehaviour
{
    [SerializeField] private List<Material> _materials;
    [SerializeField] private Material _defaultMaterial;

    private Renderer _renderer;
    private bool _isTouched;
    private float _minTime = 2;
    private float _maxTime = 5;
    private Vector3 _startPosition;
    private WaitForSeconds _delay;

    public event UnityAction<Cube> TimeIsOver;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _startPosition = transform.position;
        _delay = ChooseTimeDelay();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Platform platform) && _isTouched == false)
        {
            _isTouched = true;
            SetRandomMaterial();
            StartCoroutine(CountTime());
        }
    }

    public void SetInitial()
    {
        gameObject.transform.position = _startPosition;
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
        _renderer.sharedMaterial = _defaultMaterial;
        _isTouched = false;
    }

    private void SetRandomMaterial()
    {
        _renderer.sharedMaterial = _materials[Random.Range(0, _materials.Count)];
    }

    private WaitForSeconds ChooseTimeDelay()
    {
        return new WaitForSeconds(Random.Range(_minTime, _maxTime + 1));
    }

    private IEnumerator CountTime()
    {
        yield return _delay;

        TimeIsOver?.Invoke(this);
    }
}
