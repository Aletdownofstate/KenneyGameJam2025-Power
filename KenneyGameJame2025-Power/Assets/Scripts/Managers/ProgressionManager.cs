using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private GameObject powerPlantBtn;
    [SerializeField] private GameObject apartmentBtn;
    [SerializeField] private GameObject shopBtn;
    [SerializeField] private GameObject parkBtn;
    [SerializeField] private GameObject houseBtn;

    private List<GameObject> buttons;

    private void Start()
    {
        buttons = new List<GameObject> { powerPlantBtn, apartmentBtn, shopBtn, parkBtn, houseBtn };

        foreach (var button in buttons)
        {
            button.SetActive(false);
        }

        if (!powerPlantBtn.activeInHierarchy)
        {
            powerPlantBtn.SetActive(true);
        }
    }

    private void OnEnable()
    {
        PlacementSystem.onPowerPlantPlaced += ActivateApartmentButton;
        PopulationManager.onFirstBuildingMilestone += ActivateShopBtn;
        PopulationManager.onSecondBuildingMilestone += ActivateParkBtn;
        PopulationManager.onThirdBuildingMilestone += ActivateHouseBtn;
    }

    private void OnDisable()
    {
        PlacementSystem.onPowerPlantPlaced -= ActivateApartmentButton;
        PopulationManager.onFirstBuildingMilestone -= ActivateShopBtn;
        PopulationManager.onSecondBuildingMilestone -= ActivateParkBtn;
        PopulationManager.onThirdBuildingMilestone -= ActivateHouseBtn;
    }

    private void ActivateApartmentButton()
    {
        if (!apartmentBtn.activeInHierarchy) apartmentBtn.SetActive(true);
    }

    private void ActivateShopBtn()
    {
        if (!shopBtn.activeInHierarchy) shopBtn.SetActive(true);        
    }

    private void ActivateParkBtn()
    {
        if (!parkBtn.activeInHierarchy) parkBtn.SetActive(true);
    }

    private void ActivateHouseBtn()
    {
        if (!houseBtn.activeInHierarchy) houseBtn.SetActive(true);
    }
}