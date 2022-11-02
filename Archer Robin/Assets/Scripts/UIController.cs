using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;


public class UIController : MonoBehaviour
{
    public enum GameState { MainMenu, Paused, Playing, GameOver, Victory, OptionMenu};
    public GameState currentState;
    public GameObject youWonPanel, gameOverPanel, allUIGame, mainMenuPanel, pausedMenuPanel,optionMenuPanel;
    AudioManager aM;
    Manager manager;

    public TextMeshProUGUI coinText;

    private void Awake()
    {
        manager = FindObjectOfType<Manager>();
        aM = FindObjectOfType<AudioManager>();
        if (SceneManager.GetActiveScene().name == "0StartMenu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.Playing);
        }
    }

    private void Update()
    {
        CheckInputs();
        UpdateCoins();
        WonPanel();
    }
    public void CheckGameState(GameState newGameState)
    {
        currentState = newGameState;
        switch (currentState)
        {
            case GameState.MainMenu:
                MainMenuSetup();
                break;
            case GameState.Paused:
                Manager.gamePaused = true;
                Time.timeScale = 0f;
                GamePaused();
                break;
            case GameState.Playing:
                Manager.gamePaused = false;
                Time.timeScale = 1f;
                GameActive();
                break;
            case GameState.GameOver:
                Manager.gamePaused = true;
                Time.timeScale = 0f;
                GameOver();
                break;
            case GameState.Victory:
                Manager.gamePaused = true;
                Time.timeScale = 0f;
                Victory();
                break;
            case GameState.OptionMenu:
                Manager.gamePaused = true;
                Time.timeScale = 0f;
                OptionMenuSetup();
                break;
        }
    }
    public void MainMenuSetup()
    {
        youWonPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
        allUIGame.SetActive(false);
        pausedMenuPanel.SetActive(false);
        optionMenuPanel.SetActive(false);
    }

    public void GameActive()
    {
        youWonPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        allUIGame.SetActive(true);
        pausedMenuPanel.SetActive(false);
        optionMenuPanel.SetActive(false);
    }

    public void GamePaused()
    {
        youWonPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        allUIGame.SetActive(false);
        pausedMenuPanel.SetActive(true);
        optionMenuPanel.SetActive(false);
    }
    public void GameOver()
    {
        youWonPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        mainMenuPanel.SetActive(false);
        allUIGame.SetActive(false);
        pausedMenuPanel.SetActive(false);
        optionMenuPanel.SetActive(false);
    }
    public void Victory()
    {
        youWonPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        allUIGame.SetActive(false);
        pausedMenuPanel.SetActive(false);
        optionMenuPanel.SetActive(false);
    }

    public void OptionMenuSetup()
    {
        youWonPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        mainMenuPanel.SetActive(false);
        allUIGame.SetActive(false);
        pausedMenuPanel.SetActive(false);
        optionMenuPanel.SetActive(true);
    }

    void CheckInputs()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Playing)
            {
                CheckGameState(GameState.Paused);
            }
            else if (currentState == GameState.Paused)
            {
                CheckGameState(GameState.Playing);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("1Level");
        CheckGameState(GameState.Playing);
    }

    public void PauseGame()
    {
        CheckGameState(GameState.Paused);
    }

    public void ResumeGame()
    {
        CheckGameState(GameState.Playing);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("0StartMenu");
        CheckGameState(GameState.MainMenu);
    }

    public void GoToOptionMenu()
    {
        CheckGameState(GameState.OptionMenu);
    }

    public void WonPanel()
    {
        if (Manager.coins >= 25)
        {
            CheckGameState(GameState.Victory);
        }
    }

    public void ResetCoins()
    {
        Manager.coins = 0;
    }

    public void BackButton()
    {
        if(SceneManager.GetActiveScene().name == "0StartMenu")
        {
            CheckGameState(GameState.MainMenu);
        }
        else
        {
            CheckGameState(GameState.Paused);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void SoundOnClick()
    {
        aM.PlaySound("ButtonClick");
    }

    public void UpdateCoins()
    {
        coinText.text = Manager.coins.ToString();
    }
}
