using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverMenuUiController : MonoBehaviour
{
    [SerializeField]
    private Canvas m_canvas;

    [SerializeField]
    private Button m_mainMenuButton;

    [SerializeField]
    private TextMeshProUGUI m_winnerNameText;

    public event System.Action MainMenuButtonPressed = delegate { };

    public void SetWinnerName(string winnerName)
    {
        m_winnerNameText.text = $"{winnerName} wins!";
    }

    public void Activate()
    {
        m_canvas.enabled = true;
        m_mainMenuButton.onClick.AddListener(OnMainMenuButtonPressed);
    }

    public void Deactivate()
    {
        m_mainMenuButton.onClick.RemoveListener(OnMainMenuButtonPressed);
        m_canvas.enabled = false;
        m_winnerNameText.text = "";
    }

    private void OnMainMenuButtonPressed()
    {
        MainMenuButtonPressed.Invoke();
    }
}
