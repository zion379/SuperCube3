using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    // game over panel
    public GameObject GameOverPanel;
    public TMP_Text FinalScoreText;

    public void ShowGameOverPanel(int score)
    {
        FinalScoreText.text = "Score: " + score.ToString();
        GameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        GameOverPanel.SetActive(false);
    }

    // main menu
    public GameObject MainMenuPanel;
    public void CloseMainMenu()
    {
        MainMenuPanel.SetActive(false);
    }

    public void OpenMainMenu()
    {
        MainMenuPanel.SetActive(true);
    }

    //In game panel
    public GameObject InGamePanel;
    public void ShowInGamePanel()
    {
        InGamePanel.SetActive(true);
    }

    public void HideInGamePanel()
    {
        InGamePanel.SetActive(false);
    }

    //pause panel
    public GameObject pausePanel;
    public void showPausePanel()
    {
        pausePanel.SetActive(true);
    }

    public void hidePausePanel()
    {
        pausePanel.SetActive(false);
    }

    //settings panel
    public GameObject settingsPanel;
    public void showSettingsPanel()
    {
        settingsPanel.SetActive(true);
    }

    public void hideSettingsPanel()
    {
        settingsPanel.SetActive(false);
    }

    //LeaderBoard Panel
    public GameObject leaderBoardPanel;
    public void showLeaderBoardPanel()
    {
        leaderBoardPanel.SetActive(true);
    }

    public void hideLeaderBoardPanel()
    {
        leaderBoardPanel.SetActive(false);
    }

    //playerCustomization Panel
    public GameObject playerCustomizationPanel;
    public void showPlayerCustumizationPanel()
    {
        playerCustomizationPanel.SetActive(true);
    }

    public void hidePlayerCustumizationPanel()
    {
        playerCustomizationPanel.SetActive(false);
    }

    //Levels Menu Panel
    public GameObject LevelsMenuPanel;
    public void showLevelsMenuPanel()
    {
        LevelsMenuPanel.SetActive(true);
    }

    public void hideLevelsMenuPanel()
    {
        LevelsMenuPanel.SetActive(false);
    }
}
