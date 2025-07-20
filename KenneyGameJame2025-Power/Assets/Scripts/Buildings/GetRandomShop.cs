using System.Collections.Generic;
using UnityEngine;

public class GetRandomShop : MonoBehaviour
{
    [SerializeField] private GameObject optionA, optionB, optionC;
    private List<GameObject> buildings;

    private void Awake()
    {
        buildings = new List<GameObject> { optionA, optionB, optionC };

        foreach (var building in buildings)
        {
            building.SetActive(false);
        }
    }

    public void SetVariant(int index)
    {
        if (index < 0 || index >= buildings.Count) return;

        foreach (var building in buildings)
        {
            building.SetActive(false);
        }

        buildings[index].SetActive(true);
    }

    public int GetRandomVariant()
    {
        return Random.Range(0, buildings.Count);
    }
}
