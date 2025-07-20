using UnityEngine;
using TMPro;
using System;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance { get; private set; }

    [SerializeField] private GameObject popUp;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private TextMeshProUGUI popUpText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    [SerializeField] private PopUpMessagesSO popUpMessages;

    private bool firstPlacement = true;

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
        PopulationManager.onFourthBuildingMilestone += () => ShowTextPopUp(5);

        GeneratePower.onPowerPlantPlaced += () => PowerPlantPlaced(4);

        MoodManager.onGameOverTimer += () => ShowTextPopUp(7);
        MoodManager.onGameOver += () => GameOver(6);
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

    private void PowerPlantPlaced(int index)
    {
        if (!firstPlacement) return;

        firstPlacement = false;

        ShowTextPopUp(index);
    }

    private void GameOver(int index)
    {
        gameOver.SetActive(true);
        gameOverText.text = popUpMessages.popupData[index].PopUpText;

        Time.timeScale = 0;
    }
}