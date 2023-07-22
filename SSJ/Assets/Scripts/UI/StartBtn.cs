using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class StartBtn : MonoBehaviour
{
    [SerializeField] private GameObject ship;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Image mars;
    [SerializeField] private TextMeshProUGUI prestartText;
    public void StartGame()
    {
        ship.SetActive(true);
        menuPanel.SetActive(false);
        mars.GetComponent<Animator>().SetBool("start", true);
    }

    public void Prestart()
    {
        prestartText.gameObject.SetActive(true);
    }
    public void NextScene()
    {
        SceneManager.LoadScene("LocationForest", LoadSceneMode.Single);
    }
}
