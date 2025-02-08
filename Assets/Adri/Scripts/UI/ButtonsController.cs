using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private GameObject pauseScreen; 
    [SerializeField] private GameObject HUDScreen; 

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }

    public void OnPauseButtonClicked()
    {
        Time.timeScale = 0f;
        pauseScreen.SetActive(true);
        HUDScreen.SetActive(false);
    }

    public void OnBackButtonClicked()
    {
        Time.timeScale = 1f;
        pauseScreen.SetActive(false);
        HUDScreen.SetActive(true);
    }
}
