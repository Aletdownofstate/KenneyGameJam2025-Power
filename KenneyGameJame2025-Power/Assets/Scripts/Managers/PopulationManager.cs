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

    public static event Action onPopulationMilestone;
    public static event Action onFirstBuildingMilestone, onSecondBuildingMilestone, onThirdBuildingMilestone;    

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
    }

    public void RemoveRandomPopulation(int max)
    {
        currentPopulation -= UnityEngine.Random.Range(10, max);

        if (currentPopulation < 0) currentPopulation = 0;        
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

        if (currentPopulation >= 250 && !secondBuidingMilestone)
        {
            secondBuidingMilestone = true;
            onSecondBuildingMilestone?.Invoke();
        }

        if (currentPopulation >= 500 && !thirdBuildingMilestone)
        {
            thirdBuildingMilestone = true;
            onThirdBuildingMilestone?.Invoke();
        }
    }
}