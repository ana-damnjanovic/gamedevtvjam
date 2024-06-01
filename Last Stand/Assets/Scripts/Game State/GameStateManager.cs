using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameStateMachine m_stateMachine;
    private StartMenuGameState m_startMenuState;
    private GameplayGameState m_gameplayState;

    public void Initialize()
    {
        m_startMenuState = new StartMenuGameState();
        m_startMenuState.PlayGameRequested += OnStartGameRequested;
        m_gameplayState = new GameplayGameState();
        m_stateMachine = new GameStateMachine();

        m_stateMachine.ChangeState( m_startMenuState );
    }

    private void OnStartGameRequested()
    {
        m_stateMachine.ChangeState( m_gameplayState );
    }
}
