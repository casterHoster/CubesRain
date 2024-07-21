using TMPro;
using UnityEngine;

public abstract class Display : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI AllCount;
    [SerializeField] protected TextMeshProUGUI OnSceneCount;

    protected abstract void UpdateCount();
}
