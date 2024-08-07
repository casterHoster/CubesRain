using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(BoxCollider))]
public class Cube : Item<Cube>
{
    [SerializeField] private List<Material> _materials;
    [SerializeField] private Material _defaultMaterial;

    private bool _isTouched;
    private Vector3 _startPosition;
    private WaitForSeconds _delay;

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        _startPosition = transform.position;
        _delay = ChooseTimeDelay();
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.TryGetComponent(out Platform platform) && _isTouched == false)
        {
            _isTouched = true;
            SetRandomMaterial();
            StartCoroutine(Count());
        }
    }

    public override void Initialize()
    {
        transform.position = _startPosition;
        gameObject.SetActive(true);
    }

    public override void Disable()
    {
        gameObject.SetActive(false);
        Renderer.sharedMaterial = _defaultMaterial;
        _isTouched = false;
    }

    protected override IEnumerator Count()
    {
        yield return _delay;

        Implemented?.Invoke(this);
    }

    private void SetRandomMaterial()
    {
        Renderer.sharedMaterial = _materials[Random.Range(0, _materials.Count)];
    }

    private WaitForSeconds ChooseTimeDelay()
    {
        return new WaitForSeconds(Random.Range(MinTime, MaxTime + 1));
    }
}
