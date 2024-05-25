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
        m_inputActions.Gameplay.Jump.performed += OnJumpPerformed;
    }

    private void FixedUpdate()
    {
        if (null != m_inputActions)
        {
            Vector2 movement = m_inputActions.Gameplay.Movement.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);
            if (moveDirection.sqrMagnitude > 0f)
            {
                m_rigidbody.AddForce(moveDirection * 10f);
                transform.forward = Vector3.Slerp(transform.forward, moveDirection, 0.15f);
            }
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded())
            {
                m_rigidbody.AddForce(Physics.gravity * -30f);
            }
            else
            {
                Debug.Log("not grounded");
            }
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 0.5f);
    }
}
