using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState 
{
    string StateName { get; }
    void OnEnter(string previous);

    void OnExit(string next);
}
