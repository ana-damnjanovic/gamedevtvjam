using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuGameState : IGameState
{
    private static string STATE_NAME = "StartMenuGameState";
    public string StateName => STATE_NAME;

    public event System.Action StartGameRequested = delegate { };

    private StartMenuUiController m_startMenuUiController;

    public StartMenuGameState()
    {
        m_startMenuUiController = GameObject.FindObjectOfType<StartMenuUiController>();
    }

    public void OnEnter(string previous)
    {
        m_startMenuUiController.PlayGameButtonPressed += OnPlayGameRequested;
        m_startMenuUiController.Activate();
    }

    public void OnExit(string next)
    {
        m_startMenuUiController.PlayGameButtonPressed -= OnPlayGameRequested;
        m_startMenuUiController.Deactivate();
    }

    private void OnPlayGameRequested()
    {
        StartGameRequested.Invoke();
    }
}
