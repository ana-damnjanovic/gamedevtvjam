using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    private RagdollPlayerController[] m_playerControllers;

    public GameplayGameState()
    {
        m_playerControllers = GameObject.FindObjectsOfType<RagdollPlayerController>();
    }

    public void OnEnter(string previous)
    {
        int numControllers = m_playerControllers.Length;
        for (int iController = 0; iController < numControllers; ++iController)
        {
            m_playerControllers[iController].Initialize();
        }
    }

    public void OnExit(string next)
    {
        
    }
}
