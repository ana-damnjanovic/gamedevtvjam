using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverMenuGameState : IGameState
{
    private static string STATE_NAME = "GameOverMenuGameState";
    public string StateName => STATE_NAME;

    private GameOverMenuUiController m_gameOverMenuUiController;

    public event System.Action MainMenuRequested = delegate { };

    public GameOverMenuGameState()
    {
        m_gameOverMenuUiController = GameObject.FindObjectOfType<GameOverMenuUiController>();
    }

    public void SetWinnerName(string winnerName)
    {
        m_gameOverMenuUiController.SetWinnerName(winnerName);
    }

    public void OnEnter(string previous)
    {
        m_gameOverMenuUiController.MainMenuButtonPressed += OnMainMenuRequested;
        m_gameOverMenuUiController.Activate();
    }

    public void OnExit(string next)
    {
        m_gameOverMenuUiController.MainMenuButtonPressed += OnMainMenuRequested;
        m_gameOverMenuUiController.Deactivate();
    }

    private void OnMainMenuRequested()
    {
        MainMenuRequested.Invoke();
    }
}
