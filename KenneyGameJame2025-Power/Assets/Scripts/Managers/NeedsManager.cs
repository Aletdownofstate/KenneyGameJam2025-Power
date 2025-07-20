using UnityEngine;
using UnityEngine.UI;

public class NeedsManager : MonoBehaviour
{
    public static NeedsManager Instance { get; private set; }

    [SerializeField] private Image housingNeed;
    [SerializeField] private Image shopNeed;
    [SerializeField] private Image parkNeed;
    [SerializeField] private Image landfillNeed;

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

    private void Start()
    {
        housingNeed.fillAmount = 0.5f;
        shopNeed.fillAmount = 0.2f;
        parkNeed.fillAmount = 0.1f;
        landfillNeed.fillAmount = 0.1f;
    }

    private void OnEnable()
    {
        PopulationManager.onPopulationAdded += () => DecreaseHousingNeed(0.15f);
        PopulationManager.onPopulationAdded += () => IncreaseLandfillNeed(0.05f);
        PopulationManager.onPopulationAdded += () => IncreaseShopNeed(0.1f);
        PopulationManager.onPopulationAdded += () => IncreaseParkNeed(0.05f);
        PopulationManager.onTickerPopulationAdded += () => IncreaseHousingNeed(0.5f);

        OnParkPlacement.onParkPlaced += () => IncreaseHousingNeed(0.15f);
        OnParkPlacement.onParkPlaced += () => DecreaseParkNeed(0.15f);

        OnShopPlacement.onShopPlaced += () => DecreaseShopNeed(0.1f);

        OnLandfillPlacement.onLandfillPlaced += () => DecreaseLandfillNeed(0.25f);

        GeneratePower.onPowerPlantPlaced += () => IncreaseHousingNeed(0.25f);
    }

    public void IncreaseHousingNeed(float amount)
    {
        housingNeed.fillAmount += amount;
    }

    public void DecreaseHousingNeed(float amount)
    {
        housingNeed.fillAmount -= amount;
    }

    public void IncreaseParkNeed(float amount)
    {
        parkNeed.fillAmount += amount;
    }

    public void DecreaseParkNeed(float amount)
    {
        parkNeed.fillAmount -= amount;
    }

    public void IncreaseShopNeed(float amount)
    {
        shopNeed.fillAmount += amount;
    }

    public void DecreaseShopNeed(float amount)
    {
        shopNeed.fillAmount -= amount;
    }

    public void IncreaseLandfillNeed(float amount)
    {
        landfillNeed.fillAmount += amount;
    }

    public void DecreaseLandfillNeed(float amount)
    {
        landfillNeed.fillAmount -= amount;
    }
}