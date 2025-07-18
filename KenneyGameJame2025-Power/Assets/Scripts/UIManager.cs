using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI availablePowerText, maxPowerPlants;
    [SerializeField] GameObject optionsScreen;

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

        PlacementSystem.onEscPressed += ShowOptions;
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

    private void ShowOptions()
    {
        if (!optionsScreen.activeInHierarchy)
        {
            optionsScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            optionsScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
