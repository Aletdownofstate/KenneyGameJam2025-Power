using System;
using UnityEngine;

public class MoodManager : MonoBehaviour
{
    public static MoodManager Instance { get; private set; }

    public enum Mood { VeryUnhappy, Unhappy, Neutral, Happy, VeryHappy }
    public Mood currentMood;

    public int moodValue;

    public static event Action onMoodChange;

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
        UpdateMoodLevel();
    }

    private void OnEnable()
    {
        onMoodChange += MoodChanges;
    }

    private void OnDisable()
    {
        onMoodChange -= MoodChanges;
    }

    public void IncreaseMood(int amount)
    {
        moodValue += amount;
        UpdateMoodLevel();
    }

    public void DecreaseMood(int amount)
    {
        moodValue -= amount;
        UpdateMoodLevel();
    }

    private void UpdateMoodLevel()
    {
        if (moodValue <= -10) currentMood = Mood.VeryUnhappy;        
        
        else if (moodValue <= -5) currentMood = Mood.Unhappy;

        else if (moodValue > -4 && moodValue < 4) currentMood = Mood.Neutral;

        else if (moodValue >= 5 && moodValue < 10) currentMood = Mood.Happy;

        else if (moodValue >= 10) currentMood = Mood.VeryHappy;

        onMoodChange?.Invoke();
    }

    private void MoodChanges()
    {
        switch (currentMood)
        {
            case Mood.VeryUnhappy:
                PopulationManager.Instance.RemoveRandomPopulation(150);
                break;

            case Mood.Unhappy:
                PopulationManager.Instance.RemoveRandomPopulation(100);
                break;

            case Mood.Neutral:
                break;

            case Mood.Happy:
                PopulationManager.Instance.AddRandomPopulation(50);
                break;

            case Mood.VeryHappy:
                PopulationManager.Instance.AddRandomPopulation(150);
                break;
        }
    }
}