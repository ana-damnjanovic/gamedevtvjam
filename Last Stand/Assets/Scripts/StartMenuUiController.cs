using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Button m_playButton;

    public event System.Action PlayGameRequested = delegate { };

    public void Activate()
    {
        m_canvas.enabled = true;
        m_playButton.onClick.AddListener(OnPlayButtonPressed);
    }

    public void Deactivate()
    {
        m_playButton.onClick.RemoveListener(OnPlayButtonPressed);
        m_canvas.enabled = false;
    }

    private void OnPlayButtonPressed()
    {
        PlayGameRequested.Invoke();
    }
}
