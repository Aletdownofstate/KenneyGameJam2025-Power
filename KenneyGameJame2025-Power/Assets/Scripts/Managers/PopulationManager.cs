using UnityEngine;
using System;

public class PopulationManager : MonoBehaviour
{
    public static PopulationManager Instance { get; private set; }

    public float currentPopulation;

    private bool firstPowerMilestone = false;
    private bool secondPowerMilestone = false;
    private bool thirdPowerMilestone = false;

    private bool firstBuidingMilestone = false;
    private bool secondBuidingMilestone = false;
    private bool thirdBuildingMilestone = false;
    private bool fourthBuildingMilestone = false;

    public static event Action onPopulationMilestone;
    public static event Action onFirstBuildingMilestone, onSecondBuildingMilestone, onThirdBuildingMilestone, onFourthBuildingMilestone;
    public static event Action onPopulationAdded, onPopulationRemoved;

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

    public void AddRandomPopulation(int max)
    {
        currentPopulation += UnityEngine.Random.Range(10, max);

        PopulationPowerMilestones();
        PopulationBuildingMilestones();

        onPopulationAdded?.Invoke();
    }

    public void RemoveRandomPopulation(int max)
    {
        currentPopulation -= UnityEngine.Random.Range(10, max);

        if (currentPopulation < 0) currentPopulation = 0;

        onPopulationRemoved?.Invoke();
    }

    private void PopulationPowerMilestones()
    {
        if (currentPopulation >= 500 && !firstPowerMilestone)
        {
            firstPowerMilestone = true;
            onPopulationMilestone?.Invoke();
        }

        if (currentPopulation >= 1000 && !secondPowerMilestone)
        {
            secondPowerMilestone = true;
            onPopulationMilestone?.Invoke();
        }

        if (currentPopulation >= 2500 && !thirdPowerMilestone)
        {
            thirdPowerMilestone = true;
            onPopulationMilestone?.Invoke();
        }
    }

    private void PopulationBuildingMilestones()
    {
        if (currentPopulation >= 100 && !firstBuidingMilestone)
        {
            firstBuidingMilestone = true;
            onFirstBuildingMilestone?.Invoke();
        }

        if (currentPopulation >= 200 && !secondBuidingMilestone)
        {
            secondBuidingMilestone = true;
            onSecondBuildingMilestone?.Invoke();
        }

        if (currentPopulation >= 350 && !thirdBuildingMilestone)
        {
            thirdBuildingMilestone = true;
            onThirdBuildingMilestone?.Invoke();
        }

        if (currentPopulation >= 600 && !fourthBuildingMilestone)
        {
            fourthBuildingMilestone = true;
            onFourthBuildingMilestone?.Invoke();
        }
    }
}