using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject newGameBtn, optionsBtn, quitBtn, logo;

    private List<GameObject> menuButtons;

    private bool isOptionsMenuOpen = false;

    private void Start()
    {
        menuButtons = new List<GameObject> { newGameBtn, optionsBtn, quitBtn, logo };
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }   
    
    public void ToggleOptions()
    {
        if (!isOptionsMenuOpen)
        {
            optionsMenu.SetActive(true);
            isOptionsMenuOpen = true;

            foreach (var button in menuButtons)
            {
                button.SetActive(false);
            }
        }
        else
        {
            optionsMenu.SetActive(false);
            isOptionsMenuOpen = false;

            foreach (var button in menuButtons)
            {
                button.SetActive(true);
            }
        }
    }
}