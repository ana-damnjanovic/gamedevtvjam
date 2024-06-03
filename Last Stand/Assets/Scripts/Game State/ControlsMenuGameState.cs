using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsMenuGameState : IGameState
{
    private static string STATE_NAME = "ControlsMenuGameState";
    public string StateName => STATE_NAME;

    public event System.Action PlayGameRequested = delegate { };

    private ControlsMenuUiController m_controlsMenuUiController;

    public ControlsMenuGameState()
    {
        m_controlsMenuUiController = GameObject.FindObjectOfType<ControlsMenuUiController>();
    }

    public void OnEnter(string previous)
    {
        m_controlsMenuUiController.PlayersReady += OnPlayersReady;
        m_controlsMenuUiController.Activate();
    }

    public void OnExit(string next)
    {
        m_controlsMenuUiController.PlayersReady -= OnPlayersReady;
        m_controlsMenuUiController.Deactivate();
    }

    private void OnPlayersReady()
    {
        PlayGameRequested.Invoke();
    }
}
