using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TextMeshProUGUI availablePowerText;
    [SerializeField] private TextMeshProUGUI maxPowerPlantsText;
    [SerializeField] private TextMeshProUGUI populationText;
    [SerializeField] private TextMeshProUGUI publicMoodText;

    [Header("Options")]
    [SerializeField] GameObject optionsScreen;
    [SerializeField] GameObject buttons, text, meters, images, needs;

    private List<GameObject> gameUiElements;

    private void Start()
    {
        gameUiElements = new List<GameObject> { buttons, text, meters, images, needs };

        availablePowerText.text = $"{ResourceManager.Instance.availablePower.ToString()}";
        maxPowerPlantsText.text = $"{ResourceManager.Instance.currentPowerPlants}/{ResourceManager.Instance.maximumPowerPlants}";
        populationText.text = $"{PopulationManager.Instance.currentPopulation}";
        publicMoodText.text = $"Public Mood: {MoodManager.Instance.currentMood}";
    }

    private void OnEnable()
    {
        GeneratePower.onPowerIncrease += UpdateAvailablePower;
        GeneratePower.onPowerPlantPlaced += UpdateAvailablePowerPlants;
        PopulationManager.onPopulationMilestone += UpdateAvailablePowerPlants;
        PlacementSystem.onBuildingPlaced += UpdateAvailablePower;

        OnApartmentPlacement.onApartmentPlaced += UpdatePopulation;
        OnHousePlacement.onHousePlaced += UpdatePopulation;
        PopulationManager.onPopulationAdded += UpdatePopulation;
        PopulationManager.onTickerPopulationAdded += UpdatePopulation;

        MoodManager.onMoodChange += UpdateMood;
        GeneratePower.onPowerPlantPlaced += UpdateMood;
        OnParkPlacement.onParkPlaced += UpdateMood;
        OnHousePlacement.onHousePlaced += UpdateMood;
        OnLandfillPlacement.onLandfillPlaced += UpdateMood;

        PlacementSystem.onEscPressed += ShowOptions;
    }

    private void OnDisable()
    {
        GeneratePower.onPowerIncrease -= UpdateAvailablePower;
        GeneratePower.onPowerPlantPlaced -= UpdateAvailablePowerPlants;
        PopulationManager.onPopulationMilestone -= UpdateAvailablePowerPlants;
        PlacementSystem.onBuildingPlaced -= UpdateAvailablePower;

        OnApartmentPlacement.onApartmentPlaced -= UpdatePopulation;
        OnHousePlacement.onHousePlaced -= UpdatePopulation;
        PopulationManager.onPopulationAdded -= UpdatePopulation;
        PopulationManager.onTickerPopulationAdded -= UpdatePopulation;

        MoodManager.onMoodChange -= UpdateMood;
        GeneratePower.onPowerPlantPlaced -= UpdateMood;
        OnParkPlacement.onParkPlaced -= UpdateMood;
        OnHousePlacement.onHousePlaced -= UpdateMood;
        OnLandfillPlacement.onLandfillPlaced -= UpdateMood;

        PlacementSystem.onEscPressed -= ShowOptions;
    }

    private void UpdateAvailablePower()
    {
        availablePowerText.text = $"{ResourceManager.Instance.availablePower.ToString()}";
    }

    private void UpdateAvailablePowerPlants()
    {
        maxPowerPlantsText.text = $"{ResourceManager.Instance.currentPowerPlants}/{ResourceManager.Instance.maximumPowerPlants}";
    }

    private void UpdatePopulation()
    {
        populationText.text = $"{PopulationManager.Instance.currentPopulation}";
    }

    private void UpdateMood()
    {
        publicMoodText.text = $"Public Mood: {MoodManager.Instance.currentMood}";
    }

    private void ShowOptions()
    {
        if (!optionsScreen.activeInHierarchy)
        {
            foreach (var element in gameUiElements)
            {
                element.SetActive(false);
            }

            AudioManager.Instance.ApplyLowPassFilter();

            optionsScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            foreach (var element in gameUiElements)
            {
                element.SetActive(true);
            }

            AudioManager.Instance.BypassLowPassFilter();

            optionsScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
