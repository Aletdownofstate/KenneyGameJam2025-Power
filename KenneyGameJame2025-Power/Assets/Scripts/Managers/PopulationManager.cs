using UnityEngine;
using System;

public class PopulationManager : MonoBehaviour
{
    public static PopulationManager Instance { get; private set; }

    public float currentPopulation;

    private bool firstMilestone = false;
    private bool secondMilestone = false;
    private bool thirdMilestone = false;

    public static event Action onPopulationMilestone;

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

        PopulationMilestones();
    }

    public void RemoveRandomPopulation(int max)
    {
        currentPopulation -= UnityEngine.Random.Range(10, max);
    }

    private void PopulationMilestones()
    {
        if (currentPopulation >= 500 && !firstMilestone)
        {
            firstMilestone = true;
            onPopulationMilestone?.Invoke();
        }

        if (currentPopulation >= 1000 && !secondMilestone)
        {
            secondMilestone = true;
            onPopulationMilestone?.Invoke();
        }

        if (currentPopulation >= 2500 && !thirdMilestone)
        {
            thirdMilestone = true;
            onPopulationMilestone?.Invoke();
        }
    }
}