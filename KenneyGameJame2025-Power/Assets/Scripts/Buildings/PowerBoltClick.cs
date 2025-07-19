using UnityEngine;

public class PowerBoltClick : MonoBehaviour
{
    private GeneratePower powerPlant;

    private void Start()
    {
        powerPlant = GetComponentInParent<GeneratePower>();
    }

    private void OnMouseDown()
    {
        powerPlant?.TryHarvest();
    }
}
