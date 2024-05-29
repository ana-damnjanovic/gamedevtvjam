using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuGameState : IGameState
{
    private static string STATE_NAME = "MainMenuGameState";
    public string StateName => STATE_NAME;

    public event System.Action StartGameRequested = delegate { };

    public void OnEnter(string previous)
    {
        throw new System.NotImplementedException();
    }

    public void OnExit(string next)
    {
        throw new System.NotImplementedException();
    }
}
