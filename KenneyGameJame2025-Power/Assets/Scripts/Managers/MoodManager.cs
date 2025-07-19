using System;
using UnityEngine;

public class MoodManager : MonoBehaviour
{
    public static MoodManager Instance { get; private set; }

    public enum Mood { VeryUnhappy, Unhappy, Neutral, Happy, VeryHappy }
    public Mood currentMood;

    private int moodValue;

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
        if (moodValue <= -10)
        {
            currentMood = Mood.VeryUnhappy;
        }
        else if (moodValue < 0)
        {
            currentMood = Mood.Unhappy;
        }
        else if (moodValue == 0)
        {
            currentMood = Mood.Neutral;
        }
        else if (moodValue < 10)
        {
            currentMood = Mood.Happy;
        }
        else
        {
            currentMood = Mood.VeryHappy;
        }

        onMoodChange?.Invoke();
    }
}