using System;
using UnityEngine;

public class OnShopPlacement : MonoBehaviour
{
    public static event Action onShopPlaced;

    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.1f);

        foreach (var hit in colliders)
        {
            if (hit.gameObject.CompareTag("Shop"))
            {                
                MoodManager.Instance.IncreaseMood(1);
            }

            if (hit.gameObject.CompareTag("Park"))
            {
                MoodManager.Instance.IncreaseMood(1);
            }
        }

        onShopPlaced?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 1.1f);
    }
}
