using System;
using UnityEngine;

public class OnApartmentPlacement : MonoBehaviour
{
    public static event Action onApartmentPlaced;

    private void Start()
    {
        PopulationManager.Instance.AddRandomPopulation(100);

        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.1f);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.CompareTag("PowerPlant"))
            {
                Debug.Log($"Hit {hit.name}");
                MoodManager.Instance.DecreaseMood(1);
            }
        }

        onApartmentPlaced?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.1f);
    }
}
