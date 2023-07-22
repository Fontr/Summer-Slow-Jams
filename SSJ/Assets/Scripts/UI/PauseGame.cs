using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private bool isPause = false;
    [SerializeField]private GameObject pausePanel;
    [SerializeField] private GameObject deathPanel;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) { Pause();}
    }

    public void Pause()
    {
        if (isPause)
        {
            isPause = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            isPause = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
