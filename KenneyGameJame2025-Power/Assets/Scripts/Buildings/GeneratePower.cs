using System;
using System.Collections;
using UnityEngine;

public class GeneratePower : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject boltObject;

    private int powerPerInterval = 1;
    private float interval = 10f;

    private bool isReadyToHarvest = false;

    public static event Action onPowerIncrease;
    public static event Action onPowerPlantPlaced;
    public static event Action onPowerCollect;

    private void Start()
    {
        IncreasePower();
        MoodManager.Instance.DecreaseMood(5);

        onPowerPlantPlaced?.Invoke();

        StartCoroutine(PowerCycle());
    }

    private IEnumerator PowerCycle()
    {
        while (!isReadyToHarvest)
        {
            yield return new WaitForSeconds(interval);

            boltObject.SetActive(true);
            anim.SetTrigger("TriggerRise");

            isReadyToHarvest = true;
        }
    }

    public void TryHarvest()
    {
        if (isReadyToHarvest)
        {
            IncreasePower();
            isReadyToHarvest = false;

            onPowerCollect?.Invoke();

            anim.ResetTrigger("TriggerRise");
            boltObject.SetActive(false);

            StartCoroutine(PowerCycle());
        }
    }

    private void IncreasePower()
    {
        ResourceManager.Instance.AddPower(powerPerInterval);
        onPowerIncrease?.Invoke();
    }
}
