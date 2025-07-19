using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance { get; private set; }

    public int availablePower = 1;
    public int maximumPowerPlants = 1;
    public int currentPowerPlants;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void OnEnable()
    {
        GeneratePower.onPowerPlantPlacement += AddPowerPlant;
        PopulationManager.onPopulationMilestone += IncreaseAvailablePowerPlants;
    }

    private void OnDisable()
    {
        GeneratePower.onPowerPlantPlacement -= AddPowerPlant;
        PopulationManager.onPopulationMilestone -= IncreaseAvailablePowerPlants;
    }

    public void AddPower(int amount)
    {
        availablePower += amount;
    }

    public void RemovePower(int amount)
    {
        availablePower -= amount;
    }

    public void AddPowerPlant()
    {
        currentPowerPlants++;
    }

    public void IncreaseAvailablePowerPlants()
    {
        maximumPowerPlants++;
    }
}