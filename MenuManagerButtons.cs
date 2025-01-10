using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManagerButtons : MonoBehaviour
{
    public GameObject settingsPanel;
    public GameObject mainPanel;
    public GameObject playPanel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        if (!PlayerPrefs.HasKey("sensitivity"))
        {
            PlayerPrefs.SetFloat("sensitivity", 1);
        }
    }

    public void PickPlay()
    {
        playPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void PickBack()
    {
        settingsPanel.SetActive(false);
        playPanel.SetActive(false);
        mainPanel.SetActive(true);
    }

    public void PickSettings()
    {
        settingsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void ChangeSensitivity(float sens)
    {
        PlayerPrefs.SetFloat("sensitivity", sens);
    }
    
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
