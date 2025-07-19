using System;
using UnityEngine;

public class OnHousePlacement : MonoBehaviour
{
    public static event Action onHousePlacement;

    private void Start()
    {
        PopulationManager.Instance.AddRandomPopulation(25);
        MoodManager.Instance.IncreaseMood(2);

        onHousePlacement?.Invoke();
    }
}
