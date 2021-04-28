using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject loseMenu;
    [SerializeField] private GameObject winMenu;
    [SerializeField] private List<GameObject> stars;

    private int starsResult;
    
    public void OpenPauseMenu()
    {
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);
    }
    
    public void ClosePauseMenu()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
    
    public void OpenLoseMenu()
    {
        Time.timeScale = 0f;
        loseMenu.SetActive(true);
    }

    public void OpenWinMenu(int starsGained)
    {
        starsResult = starsGained;
        winMenu.SetActive(true);
        StartCoroutine(ShowStarResult(0.5f, 0.1f));
    }

    private IEnumerator ShowStarResult(float delay, float delayAmongStars)
    {
        yield return new WaitForSeconds(delay);
        for(int i = 0; i < starsResult; i++)
        {
            stars[i].SetActive(true);
            yield return new WaitForSeconds(delayAmongStars);
        }
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
