using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI availablePowerText, maxPowerPlants;

    private void Start()
    {
        availablePowerText.text = $"Available Power: {ResourceManager.Instance.availablePower.ToString()}";
        maxPowerPlants.text = $"Power Plants: {ResourceManager.Instance.currentPowerPlants}/{ResourceManager.Instance.maximumPowerPlants}";
    }

    private void OnEnable()
    {
        GeneratePower.onPowerIncrease += UpdateAvailablePower;
        GeneratePower.onPowerPlantPlacement += UpdateAvailablePowerPlants;
        PlacementSystem.onBuildingPlaced += UpdateAvailablePower;
    }

    private void OnDisable()
    {
        GeneratePower.onPowerIncrease -= UpdateAvailablePower;
        GeneratePower.onPowerPlantPlacement -= UpdateAvailablePowerPlants;
        PlacementSystem.onBuildingPlaced -= UpdateAvailablePower;
    }

    private void UpdateAvailablePower()
    {
        availablePowerText.text = $"Available Power: {ResourceManager.Instance.availablePower.ToString()}";
    }

    private void UpdateAvailablePowerPlants()
    {
        maxPowerPlants.text = $"Power Plants: {ResourceManager.Instance.currentPowerPlants}/{ResourceManager.Instance.maximumPowerPlants}";
    }
}
