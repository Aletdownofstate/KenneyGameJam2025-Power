using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu]
public class PopUpMessagesSO : ScriptableObject
{
    public List<PopUpData> popupData;
}

[Serializable]
public class PopUpData
{
    [field: SerializeField] public string PopUpText { get; private set; }
}
