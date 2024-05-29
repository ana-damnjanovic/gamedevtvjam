using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameStateMachine m_stateMachine;
    private MainMenuGameState m_startMenuState;
    private GameplayGameState m_gameplayState;

    public event System.Action GameplaySceneRequested;

    public void Initialize()
    {
        m_startMenuState = new MainMenuGameState();
        m_startMenuState.StartGameRequested += OnStartGameRequested;
        m_gameplayState = new GameplayGameState();
        m_stateMachine = new GameStateMachine();

        m_stateMachine.ChangeState( m_startMenuState );
    }

    public void StartGameplay()
    {
        m_stateMachine.ChangeState( m_gameplayState );
    }

    private void OnStartGameRequested()
    {
        GameplaySceneRequested.Invoke();
    }
}
