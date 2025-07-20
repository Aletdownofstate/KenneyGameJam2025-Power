using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject tutorialPopUp;
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private PopUpMessagesSO popUpMessages;

    private void Start()
    {
        tutorialPopUp.SetActive(true);
        tutorialText.text = popUpMessages.popupData[8].PopUpText;

        Time.timeScale = 0;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void CloseTutorial()
    {
        tutorialPopUp.SetActive(false);
        Time.timeScale = 1;
    }
}
