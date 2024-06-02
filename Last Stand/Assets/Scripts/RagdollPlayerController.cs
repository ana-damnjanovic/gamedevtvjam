using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RagdollPlayerController : MonoBehaviour
{
    [SerializeField]
    private float m_speed = 100f;
    [SerializeField]
    private float m_jumpForce = 100f;

    [SerializeField]
    private bool m_initializeOnAwake = false;

    [SerializeField]
    private Rigidbody m_hips;

    [SerializeField]
    private ConfigurableJoint m_hipJoint;

    private InputActions m_inputActions;

    public void Initialize()
    {
        m_inputActions = new InputActions();
        m_inputActions.Enable();
        m_inputActions.Gameplay.Jump.performed += OnJumpPerformed;
    }

    private void Awake()
    {
        if ( m_initializeOnAwake )
        {
            Initialize();
        }
    }

    private void FixedUpdate()
    {
        if (null != m_hips && null != m_inputActions)
        {
            Vector2 movement = m_inputActions.Gameplay.Movement.ReadValue<Vector2>();
            Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);

            if (moveDirection.sqrMagnitude > 0f)
            {
                Vector3 hipFacingDirection = new Vector3(-moveDirection.x, moveDirection.y, moveDirection.z);
                m_hipJoint.targetRotation = Quaternion.LookRotation(hipFacingDirection);
                m_hips.AddForce(moveDirection * m_speed);
            }
        }
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (IsGrounded())
            {
                m_hips.AddForce(new Vector3(0, m_jumpForce, 0));
            }
            else
            {
                Debug.Log("not grounded");
            }
        }
    }

    private bool IsGrounded()
    {
        int layerMask = LayerMask.GetMask("Ground");
        Ray ray = new Ray(m_hips.transform.position, Vector3.down);
        return Physics.Raycast(ray, 1f, layerMask, QueryTriggerInteraction.Collide);
    }
}
