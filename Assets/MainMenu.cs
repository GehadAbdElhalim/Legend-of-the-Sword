using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MainMenuPanel;
    public GameObject ControlsPanel;
    public GameObject CreditsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Back();
        }
    }


    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Controls()
    {
        MainMenuPanel.SetActive(false);
        ControlsPanel.SetActive(true);
    }

    public void Credits()
    {
        MainMenuPanel.SetActive(false);
        CreditsPanel.SetActive(true);
    }

    public void Back()
    {
        MainMenuPanel.SetActive(true);
        CreditsPanel.SetActive(false);
        ControlsPanel.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

}
