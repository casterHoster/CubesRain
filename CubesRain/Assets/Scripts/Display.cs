using TMPro;
using UnityEngine;

public abstract class Display : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI OverallQuantity;
    [SerializeField] protected TextMeshProUGUI OnSceneQuantity;

    protected abstract void UpdateCount();
}
