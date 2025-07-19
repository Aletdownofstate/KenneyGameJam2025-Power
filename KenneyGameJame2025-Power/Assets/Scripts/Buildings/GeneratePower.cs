using System;
using UnityEngine;

public class GeneratePower : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject boltObject;

    private int powerPerInterval = 1;
    private float interval = 10f;

    private bool isGenerating = false;

    public static event Action onPowerIncrease;
    public static event Action onPowerPlantPlacement;

    private void Start()
    {
        StartGeneratingPower();

        onPowerPlantPlacement?.Invoke();

        boltObject.SetActive(true);
        anim.SetTrigger("TriggerRise");
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
