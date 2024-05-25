using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    private Rigidbody m_rigidbody;

    private InputActions m_inputActions;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
        m_inputActions = new InputActions();
        m_inputActions.Enable();
    }

    private void Update()
    {
        Vector2 movement = m_inputActions.Gameplay.Movement.ReadValue<Vector2>();
        m_rigidbody.AddForce( movement.x * 5f, 0, movement.y * 5f );
    }
}
