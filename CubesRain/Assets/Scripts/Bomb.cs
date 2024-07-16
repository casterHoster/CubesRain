using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Renderer))]
public class Bomb : MonoBehaviour
{
    [SerializeField] private float _range;
    [SerializeField] private float _force;

    private Renderer _renderer;
    private float _minTime = 2;
    private float _maxTime = 5;
    private float _delay;
    private Color _color;

    public event UnityAction<Bomb> Implemented;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
        _delay = ChooseTimeDelay();
    }

    public void Implement()
    {
        foreach (Rigidbody explodableObject in GetExplodableObjects())
        {
            float distance = GetDistance(explodableObject);

            if (distance != 0)
            {
                explodableObject.AddExplosionForce(_force, transform.position, _range);
            }
        }

        Implemented?.Invoke(this);
    }

    public void SetInitial(Vector3 position)
    {
        gameObject.SetActive(true);
        transform.position = position;
        StartCoroutine(Count());
    }

    public void Disable()
    {
        _color.a = 1;
        _renderer.material.color = _color;
        gameObject.SetActive(false);
    }

    private List<Rigidbody> GetExplodableObjects()
    {
        List<Rigidbody> reachesObjects = new List<Rigidbody>();
        Collider[] hits = Physics.OverlapSphere(transform.position, _range);

        foreach (Collider hit in hits)
        {
            if (hit.attachedRigidbody != null)
            {
                reachesObjects.Add(hit.attachedRigidbody);
            }
        }

        return reachesObjects;
    }

    private float GetDistance(Rigidbody explodableObject)
    {
        return (transform.position - explodableObject.position).magnitude;
    }

    private float ChooseTimeDelay()
    {
        return Random.Range(_minTime, _maxTime);
    }

    private IEnumerator Count()
    {
        float tick = _color.a / _delay;

        while (_color.a > 0)
        {
            _color.a = Mathf.Lerp(_color.a, _color.a -= tick, Time.deltaTime);
            _renderer.material.color = _color;
            yield return null;
        }

        Implement();
    }
}
