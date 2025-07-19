using System;
using UnityEngine;

public class AddPopulationOnPlace : MonoBehaviour
{
    public static event Action onPopulationIncrease;

    private void Start()
    {
        PopulationManager.Instance.AddRandomPopulation(100);

        onPopulationIncrease?.Invoke();
    }
}
