using UnityEngine;
using TMPro;
using System;

public class PopUpManager : MonoBehaviour
{
    public static PopUpManager Instance { get; private set; }

    [SerializeField] private GameObject popUp;

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
        
    }

    private void OnDisable()
    {
        
    }

    public void ShowPopUp()
    {
        popUp.SetActive(true);
        onPopUp?.Invoke();
    }

    public void HidePopUp()
    {
        popUp.SetActive(false);
    }
}