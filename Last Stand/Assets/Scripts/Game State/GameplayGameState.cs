using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayGameState : IGameState
{
    private static string STATE_NAME = "GameplayGameState";
    public string StateName => STATE_NAME;

    private RagdollPlayerController m_playerController;

    public GameplayGameState()
    {
        m_playerController = GameObject.FindObjectOfType<RagdollPlayerController>();
    }

    public void OnEnter(string previous)
    {
        m_playerController.Initialize();
    }

    public void OnExit(string next)
    {
        
    }
}
