using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject startMenu;

    public GameObject pausedPanel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startMenu.SetActive(false);
    }

    public void OnPaused()
    {
        Time.timeScale = 0;
        pausedPanel.SetActive(true);
    }

    public void Resume()
    {
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        pausedPanel.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
