using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string m_startMenuSceneName;

    [SerializeField]
    private string m_gameplaySceneName;

    private GameStateManager m_gameStateManager;

    private void Awake()
    {
        m_gameStateManager = GameObject.FindObjectOfType<GameStateManager>();

        AsyncOperation startMenuLoadOperation = SceneManager.LoadSceneAsync(m_startMenuSceneName, LoadSceneMode.Additive);
        startMenuLoadOperation.allowSceneActivation = true;
        startMenuLoadOperation.completed += OnStartSceneLoaded;

        AsyncOperation gameplaySceneLoadOperation = SceneManager.LoadSceneAsync(m_gameplaySceneName, LoadSceneMode.Additive);
        startMenuLoadOperation.allowSceneActivation = false;
        gameplaySceneLoadOperation.completed += OnGameplaySceneLoaded;
    }

    private void OnStartSceneLoaded( AsyncOperation sceneLoadOperation )
    {
        sceneLoadOperation.completed -= OnStartSceneLoaded;
        m_gameStateManager.Initialize();
    }

    private void OnGameplaySceneLoaded( AsyncOperation sceneLoadOperation )
    {
        sceneLoadOperation.completed -= OnGameplaySceneLoaded;
    }
}
