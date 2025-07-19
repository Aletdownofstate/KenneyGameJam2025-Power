using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake()
    {
        StartCoroutine(InitialiseOptions());
    }
    
    private IEnumerator InitialiseOptions()
    {
        GameObject optionsManager = GameObject.FindGameObjectWithTag("Options");
        optionsManager.SetActive(true);
        yield return null;
        optionsManager.SetActive(false);
    }
}
