using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlsMenuUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private InputActionReference m_p1ReadyAction;

    [SerializeField]
    private GameObject m_p1ReadyToggle;

    [SerializeField]
    private InputActionReference m_p2ReadyAction;

    [SerializeField]
    private GameObject m_p2ReadyToggle;

    // delay the event slightly so players can see both checkmarks on screen
    [SerializeField]
    private float m_playerReadyDelay = 0.15f;

    public event System.Action PlayersReady = delegate { };

    public void Activate()
    {
        m_p1ReadyAction.action.performed += OnP1Ready;
        m_p1ReadyAction.action.Enable();
        m_p2ReadyAction.action.performed += OnP2Ready;
        m_p2ReadyAction.action.Enable();
        m_canvas.enabled = true;
    }

    public void Deactivate()
    {
        m_p1ReadyAction.action.performed -= OnP1Ready;
        m_p2ReadyAction.action.performed -= OnP2Ready;
        m_p1ReadyAction.action.Disable();
        m_p2ReadyAction.action.Disable();

        m_canvas.enabled = false;
        m_p1ReadyToggle.SetActive(false);
        m_p2ReadyToggle.SetActive(false);
    }

    private void OnP1Ready(InputAction.CallbackContext context)
    {
        m_p1ReadyToggle.SetActive(true);
        if (m_p2ReadyToggle.activeInHierarchy)
        {
            StartCoroutine(WaitAndInvokePlayersReady());
        }
    }

    private void OnP2Ready(InputAction.CallbackContext context)
    {
        m_p2ReadyToggle.SetActive(true);
        if (m_p1ReadyToggle.activeInHierarchy)
        {
            StartCoroutine(WaitAndInvokePlayersReady());
        }
    }

    private IEnumerator WaitAndInvokePlayersReady()
    {
        yield return new WaitForSeconds(m_playerReadyDelay);
        PlayersReady.Invoke();
    }

}
