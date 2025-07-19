using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
{
    [SerializeField] private GameObject powerPlantBtn, apartmentBtn, shopBtn, parkBtn;
    private List<GameObject> buttons;

    private void Start()
    {
        buttons = new List<GameObject> { powerPlantBtn, apartmentBtn, shopBtn, parkBtn };

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
    }

    private void OnDisable()
    {
        PlacementSystem.onPowerPlantPlaced -= ActivateApartmentButton;
        PopulationManager.onFirstBuildingMilestone -= ActivateShopBtn;
        PopulationManager.onSecondBuildingMilestone -= ActivateParkBtn;
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
}