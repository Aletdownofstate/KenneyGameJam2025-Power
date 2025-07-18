using System;
using UnityEngine;

public class GeneratePower : MonoBehaviour
{
    private int powerPerInterval = 1;
    private float interval = 10f;

    private bool isGenerating = false;

    public static event Action onPowerIncrease;
    public static event Action onPowerPlantPlacement;

    private void Start()
    {
        StartGeneratingPower();

        onPowerPlantPlacement?.Invoke();
    }

    private void StartGeneratingPower()
    {
        if (!isGenerating)
        {
            isGenerating = true;
            InvokeRepeating(nameof(Generate), interval, interval);
        }
    }

    private void Generate()
    {
        ResourceManager.Instance.AddPower(powerPerInterval);
        onPowerIncrease?.Invoke();
    }
}
