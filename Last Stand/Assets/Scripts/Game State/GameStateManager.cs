using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameStateMachine m_stateMachine;
    private StartMenuGameState m_startMenuState;
    private ControlsMenuGameState m_controlsMenuState;
    private GameplayGameState m_gameplayState;
    private GameOverMenuGameState m_gameOverState;

    public event System.Action GameResetRequested = delegate { };

    public void Initialize()
    {
        m_startMenuState = new StartMenuGameState();
        m_startMenuState.StartGameRequested += OnStartGameRequested;
        m_controlsMenuState = new ControlsMenuGameState();
        m_controlsMenuState.PlayGameRequested += OnPlayGameRequested;
        m_gameplayState = new GameplayGameState();
        m_gameplayState.EndGameRequested += OnGameOverRequested;
        m_gameOverState = new GameOverMenuGameState();
        m_gameOverState.MainMenuRequested += OnGameResetRequested;
        m_stateMachine = new GameStateMachine();

        m_stateMachine.ChangeState( m_startMenuState );
    }

    private void OnStartGameRequested()
    {
        m_stateMachine.ChangeState( m_controlsMenuState );
    }

    private void OnPlayGameRequested()
    {
        m_stateMachine.ChangeState( m_gameplayState );
    }

    private void OnGameOverRequested(string winnerName)
    {
        m_gameOverState.SetWinnerName(winnerName);
        m_stateMachine.ChangeState(m_gameOverState);
    }

    private void OnGameResetRequested()
    {
        GameResetRequested.Invoke();
    }
}
