using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuGameState : IGameState
{
    private static string STATE_NAME = "MainMenuGameState";
    public string StateName => STATE_NAME;

    public event System.Action PlayGameRequested = delegate { };

    private StartMenuUiController m_startMenuUiController;

    public StartMenuGameState()
    {
        m_startMenuUiController = GameObject.FindObjectOfType<StartMenuUiController>();
    }

    public void OnEnter(string previous)
    {
        m_startMenuUiController.PlayGameRequested += OnPlayGameRequested;
        m_startMenuUiController.Activate();
    }

    public void OnExit(string next)
    {
        m_startMenuUiController.PlayGameRequested -= OnPlayGameRequested;
        m_startMenuUiController.Deactivate();
    }

    private void OnPlayGameRequested()
    {
        PlayGameRequested.Invoke();
    }
}
