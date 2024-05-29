using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine 
{
    private Stack<IGameState> m_activeStates;

    public GameStateMachine()
    {
        m_activeStates = new Stack<IGameState>();
    }

    public void ChangeState(IGameState nextState)
    {
        IGameState currentState = m_activeStates.Peek();
        if ( null != currentState )
        {
            if ( currentState != nextState )
            {
                currentState.OnExit( nextState.StateName );
                m_activeStates.Pop();
                m_activeStates.Push( nextState );
                nextState.OnEnter( currentState.StateName );
            }
            else
            {
                Debug.LogError($"already in state {nextState.StateName}");
            }
        }
        else
        {
            m_activeStates.Push( nextState );
            nextState.OnEnter( currentState.StateName );
        }
    }
}
