using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI availablePowerText, maxPowerPlantsText, populationText, publicMoodText;
    [SerializeField] GameObject optionsScreen;

    private void Start()
    {
        availablePowerText.text = $"Available Power: {ResourceManager.Instance.availablePower.ToString()}";
        maxPowerPlantsText.text = $"Power Plants: {ResourceManager.Instance.currentPowerPlants}/{ResourceManager.Instance.maximumPowerPlants}";
        populationText.text = $"Population: {PopulationManager.Instance.currentPopulation}";
        publicMoodText.text = $"Public Mood: {MoodManager.Instance.currentMood}";
    }

    private void OnEnable()
    {
        GeneratePower.onPowerIncrease += UpdateAvailablePower;
        GeneratePower.onPowerPlantPlacement += UpdateAvailablePowerPlants;
        PopulationManager.onPopulationMilestone += UpdateAvailablePowerPlants;
        PlacementSystem.onBuildingPlaced += UpdateAvailablePower;

        AddPopulationOnPlace.onPopulationIncrease += UpdatePopulation;

        MoodManager.onMoodChange += UpdateMood;

        PlacementSystem.onEscPressed += ShowOptions;
    }

    private void OnDisable()
    {
        GeneratePower.onPowerIncrease -= UpdateAvailablePower;
        GeneratePower.onPowerPlantPlacement -= UpdateAvailablePowerPlants;
        PopulationManager.onPopulationMilestone -= UpdateAvailablePowerPlants;
        PlacementSystem.onBuildingPlaced -= UpdateAvailablePower;

        AddPopulationOnPlace.onPopulationIncrease -= UpdatePopulation;

        MoodManager.onMoodChange -= UpdateMood;

        PlacementSystem.onEscPressed -= ShowOptions;
    }

    private void UpdateAvailablePower()
    {
        availablePowerText.text = $"Available Power: {ResourceManager.Instance.availablePower.ToString()}";
    }

    private void UpdateAvailablePowerPlants()
    {
        maxPowerPlantsText.text = $"Power Plants: {ResourceManager.Instance.currentPowerPlants}/{ResourceManager.Instance.maximumPowerPlants}";
    }

    private void UpdatePopulation()
    {
        populationText.text = $"Population: {PopulationManager.Instance.currentPopulation}";
    }

    private void UpdateMood()
    {
        publicMoodText.text = $"Public Mood: {MoodManager.Instance.currentMood}";
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
