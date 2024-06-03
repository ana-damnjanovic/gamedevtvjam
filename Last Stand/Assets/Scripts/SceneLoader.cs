using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private string m_startMenuSceneName;

    [SerializeField]
    private string m_controlsMenuSceneName;

    [SerializeField]
    private string m_gameplaySceneName;

    [SerializeField]
    private string m_gameOverMenuSceneName;

    [SerializeField]
    private string m_startupSceneName;

    private GameStateManager m_gameStateManager;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_gameStateManager = GameObject.FindObjectOfType<GameStateManager>();
        m_gameStateManager.GameResetRequested += OnResetRequested;

        AsyncOperation startMenuLoadOperation = SceneManager.LoadSceneAsync(m_startMenuSceneName, LoadSceneMode.Additive);
        startMenuLoadOperation.allowSceneActivation = true;
        startMenuLoadOperation.completed += OnStartSceneLoaded;

        AsyncOperation controlsMenuLoadOperation = SceneManager.LoadSceneAsync( m_controlsMenuSceneName, LoadSceneMode.Additive );

        AsyncOperation gameplaySceneLoadOperation = SceneManager.LoadSceneAsync(m_gameplaySceneName, LoadSceneMode.Additive);
        gameplaySceneLoadOperation.allowSceneActivation = false;
        gameplaySceneLoadOperation.completed += OnGameplaySceneLoaded;

        AsyncOperation gameOverSceneLoadOperation = SceneManager.LoadSceneAsync(m_gameOverMenuSceneName, LoadSceneMode.Additive);
        gameOverSceneLoadOperation.allowSceneActivation = false;
        gameOverSceneLoadOperation.completed += OnGameOverSceneLoaded;
    }

    private void OnStartSceneLoaded( AsyncOperation sceneLoadOperation )
    {
        sceneLoadOperation.completed -= OnStartSceneLoaded;
    }

    private void OnGameplaySceneLoaded( AsyncOperation sceneLoadOperation )
    {
        sceneLoadOperation.completed -= OnGameplaySceneLoaded;
    }

    private void OnGameOverSceneLoaded( AsyncOperation sceneLoadOperation)
    {
        sceneLoadOperation.completed -= OnGameplaySceneLoaded;
        m_gameStateManager.Initialize();
    }

    private void OnResetRequested()
    {
        m_gameStateManager.GameResetRequested -= OnResetRequested;
        SceneManager.LoadScene(m_startupSceneName);
    }
}
