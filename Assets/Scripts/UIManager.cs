using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class UIManager : MonoBehaviour
{
    // main menu
    public GameObject MainMenuPanel;
    public void CloseMainMenu()
    {
        MainMenuPanel.active = false;
    }

    public void OpenMainMenu()
    {
        MainMenuPanel.active = true;
    }

    //In game panel
    public GameObject InGamePanel;
    public void ShowInGamePanel()
    {
        InGamePanel.active = true;
    }

    public void HideInGamePanel()
    {
        InGamePanel.active = false;
    }

    //pause panel
    public GameObject pausePanel;
    public void showPausePanel()
    {
        pausePanel.active = true;
    }

    public void hidePausePanel()
    {
        pausePanel.active = false;
    }

    //settings panel
    public GameObject settingsPanel;
    public void showSettingsPanel()
    {
        settingsPanel.active = true;
    }

    public void hideSettingsPanel()
    {
        settingsPanel.active = false;
    }

    //LeaderBoard Panel
    public GameObject leaderBoardPanel;
    public void showLeaderBoardPanel()
    {
        leaderBoardPanel.active = true;
    }

    public void hideLeaderBoardPanel()
    {
        leaderBoardPanel.active = false;
    }

    //playerCustomization Panel
    public GameObject playerCustomizationPanel;
    public void showPlayerCustumizationPanel()
    {
        playerCustomizationPanel.active = true;
    }

    public void hidePlayerCustumizationPanel()
    {
        playerCustomizationPanel.active = false;
    }

    //Levels Menu Panel
    public GameObject LevelsMenuPanel;
    public void showLevelsMenuPanel()
    {
        LevelsMenuPanel.active = true;
    }

    public void hideLevelsMenuPanel()
    {
        LevelsMenuPanel.active = false;
    }
}
