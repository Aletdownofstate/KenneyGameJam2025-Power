using System;
using UnityEngine;

public class OnLandfillPlacement : MonoBehaviour
{
    public static event Action onLandfillPlaced;

    private void Start()
    {
        MoodManager.Instance.DecreaseMood(1);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.1f);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.CompareTag("Apartment") || hit.gameObject.CompareTag("House") || hit.gameObject.CompareTag("Shop"))
            {
                Debug.Log($"Hit {hit.name}");
                MoodManager.Instance.DecreaseMood(2);
            }
        }

        onLandfillPlaced?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.1f);
    }
}
