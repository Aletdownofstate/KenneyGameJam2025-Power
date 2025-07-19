using UnityEngine;
using System;

public class OnParkPlacement : MonoBehaviour
{
    public static event Action onParkPlaced;

    private void Start()
    {
        MoodManager.Instance.IncreaseMood(1);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.1f);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.CompareTag("Apartment"))
            {
                Debug.Log($"Hit {hit.name}");
                MoodManager.Instance.IncreaseMood(1);
            }
        }

        onParkPlaced?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.1f);
    }
}