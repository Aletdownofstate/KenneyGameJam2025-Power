using UnityEngine;
using TMPro;
using System;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance { get; private set; }

    [SerializeField] private GameObject popUp;
    [SerializeField] private TextMeshProUGUI popUpText;

    [SerializeField] private PopUpMessagesSO popUpMessages;    

    public static event Action onPopUp;

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
        PopulationManager.onPopulationMilestone += () => ShowTextPopUp(0);

        PopulationManager.onFirstBuildingMilestone += () => ShowTextPopUp(1);
        PopulationManager.onSecondBuildingMilestone += () => ShowTextPopUp(2);
        PopulationManager.onThirdBuildingMilestone += () => ShowTextPopUp(3);

        GeneratePower.onPowerPlantPlaced += () => ShowTextPopUp(4);
    }    

    public void ShowTextPopUp(int index)
    {
        popUp.SetActive(true);
        popUpText.text = popUpMessages.popupData[index].PopUpText;

        Time.timeScale = 0;

        onPopUp?.Invoke();
    }

    public void HideTextPopUp()
    {
        popUp.SetActive(false);

        Time.timeScale = 1;
    }    
}