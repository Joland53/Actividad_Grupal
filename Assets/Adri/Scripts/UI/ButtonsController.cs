using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen; 
    [SerializeField] private GameObject HUDScreen;
    [SerializeField] private HealthManager healthManagerSO;
    [SerializeField] private PlayerMovement playerMovement;

    bool isPaused = false;
    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (isPaused)
                {
                    OnBackButtonClicked();
                }
                else
                {
                    OnPauseButtonClicked();
                }
                isPaused = !isPaused;
            }

    }
    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        HUDScreen.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        healthManagerSO.PauseScreen();
    }

    public void OnBackButtonClicked()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        HUDScreen.SetActive(true);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        healthManagerSO.ResumeGame();
    }
}
