using UnityEngine;

public class PopulationManager : MonoBehaviour
{
    public static PopulationManager Instance { get; private set; }

    public float currentPopulation;

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

    public void AddRandomPopulation()
    {
        currentPopulation += Random.Range(10, 150);
    }
}
