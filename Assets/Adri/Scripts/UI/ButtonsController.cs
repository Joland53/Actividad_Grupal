using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonsController : MonoBehaviour
{
    [SerializeField] private Button exitButton;

    public void OnExitButtonClicked()
    {
        SceneManager.LoadScene(0);
    }
}
