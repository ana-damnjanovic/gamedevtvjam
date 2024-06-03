using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    private RagdollPlayerController[] m_playerControllers;

    private PlayerLostDetector m_playerLostDetector;

    public event System.Action<string> EndGameRequested = delegate { };

    public GameplayGameState()
    {
        m_playerControllers = GameObject.FindObjectsOfType<RagdollPlayerController>();
        m_playerLostDetector = GameObject.FindObjectOfType<PlayerLostDetector>();
    }

    public void OnEnter(string previous)
    {
        int numControllers = m_playerControllers.Length;
        for (int iController = 0; iController < numControllers; ++iController)
        {
            m_playerControllers[iController].Initialize();
        }
        m_playerLostDetector.PlayerLost += OnPlayerLost;
    }
    
    public void OnExit(string next)
    {
        m_playerLostDetector.PlayerLost -= OnPlayerLost;
    }

    private void OnPlayerLost(RagdollPlayerController losingPlayer)
    {
        string winnerName = "";
        for (int i = 0; i < m_playerControllers.Length; ++i)
        {
            m_playerControllers[i].DisableInputs();
            if (m_playerControllers[i] != losingPlayer)
            {
                winnerName = m_playerControllers[i].gameObject.name;
            }
        }
        EndGameRequested.Invoke(winnerName);
    }
}
