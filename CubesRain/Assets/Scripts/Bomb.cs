//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Events;

//public class Bomb : MonoBehaviour
//{
//    [SerializeField] private float _range;
//    [SerializeField] private float _force;

//    private WaitForSeconds _delay;
//    private float _minTime = 2;
//    private float _maxTime = 5;

//    public event UnityAction<Cube> IsTimeOver;

//    public void Implement()
//    {
//        foreach (Rigidbody explodableObject in GetExplodableObjects())
//        {
//            float distance = GetDistance(explodableObject);

//            if (distance != 0)
//            {
//                explodableObject.AddExplosionForce(_force, transform.position, _range);
//            }
//        }

//        Destroy(gameObject);
//    }

//    private List<Rigidbody> GetExplodableObjects()
//    {
//        List<Rigidbody> reachesObjects = new List<Rigidbody>();
//        Collider[] hits = Physics.OverlapSphere(transform.position, _range);

//        foreach (Collider hit in hits)
//        {
//            if (hit.attachedRigidbody != null)
//            {
//                reachesObjects.Add(hit.attachedRigidbody);
//            }
//        }

//        return reachesObjects;
//    }

//    private float GetDistance(Rigidbody explodableObject)
//    {
//        return (transform.position - explodableObject.position).magnitude;
//    }

//    private WaitForSeconds ChooseTimeDelay()
//    {
//        return new WaitForSeconds(Random.Range(_minTime, _maxTime + 1));
//    }

//    private IEnumerator CountTime()
//    {
//        yield return _delay;

//        IsTimeOver?.Invoke(this);
//    }
//}
