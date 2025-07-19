using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class BuildingDatabasSO : ScriptableObject
{
    public List<BuildingData> buildingData;
}

[Serializable]
public class BuildingData
{
    [field: SerializeField] public int ID { get; private set; }

    [field: SerializeField] public string Name { get; private set; }

    [field: SerializeField] public Vector2Int Size { get; private set; } = Vector2Int.one;

    [field: SerializeField] public int PowerCost { get; private set; }

    [field: SerializeField] public GameObject Prefab { get; private set; }
}
