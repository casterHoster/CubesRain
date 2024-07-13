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
    private int _minTime = 2;
    private int _maxTime = 5;
    private int _delay;
    private Color _color;

    public event UnityAction<Bomb> IsTimeOver;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _color = _renderer.material.color;
        _delay = ChooseTimeDelay();
        StartCoroutine(Count());
    }

    //private void Update()
    //{
    //    Debug.Log(_color.a);
    //}

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

        Destroy(gameObject);
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

    private int ChooseTimeDelay()
    {
        return Random.Range(_minTime, _maxTime + 1);
    }

    private IEnumerator Count()
    {
        float tick = _color.a / _delay;
        WaitForSeconds oneSecond  = new WaitForSeconds(1);

        for (float i = _color.a; i > 0; i -= tick)
        {
            _color.a -= tick;
            _renderer.material.color = _color;
            yield return new WaitForSeconds(1);
        }

        IsTimeOver?.Invoke(this);
    }
}
