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

    [SerializeField]
    private LayerMask m_layerMask;

    private InputActions m_inputActions;

    [SerializeField]
    private InputActionReference m_movementAction;

    [SerializeField]
    private InputActionReference m_jumpAction;

    [SerializeField]
    private Animator m_animator;

    private bool m_isBouncing = false;

    private Coroutine m_bounceCoroutine;

    private CollisionNotifier[] m_collisionNotifiers;

    public Vector3 HipVelocity => m_hips.velocity;

    public void Initialize()
    {
        m_inputActions = new InputActions();
        m_inputActions.Enable();
        m_movementAction.action.Enable();
        m_jumpAction.action.Enable();
        m_jumpAction.action.performed += OnJumpPerformed;

        m_collisionNotifiers = GetComponentsInChildren<CollisionNotifier>();
        for (int iNotifier = 0; iNotifier < m_collisionNotifiers.Length; ++iNotifier)
        {
            m_collisionNotifiers[iNotifier].Collided += OnCollided;
        }

        m_animator.enabled = true;
    }

    public void DisableInputs()
    {
        m_jumpAction.action.performed -= OnJumpPerformed;
        m_jumpAction.action.Disable();
        m_inputActions.Disable();
        m_movementAction.action.Disable();
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
            if (!m_isBouncing)
            {
                Vector2 movement = m_movementAction.action.ReadValue<Vector2>();
                Vector3 moveDirection = new Vector3(movement.x, 0f, movement.y);

                if (moveDirection.sqrMagnitude > 0f)
                {
                    Vector3 hipFacingDirection = new Vector3(-moveDirection.x, moveDirection.y, moveDirection.z);
                    m_hipJoint.targetRotation = Quaternion.LookRotation(hipFacingDirection);
                    m_hips.AddForce(moveDirection * m_speed);
                }
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
        Debug.Log("is grounded");
        int layerMask = LayerMask.GetMask("Ground");
        Ray ray = new Ray(m_hips.transform.position, Vector3.down);
        return Physics.Raycast(ray, 1f, layerMask, QueryTriggerInteraction.Collide);
    }

    private void OnCollided(Collider collider, Collision collision)
    {
        if ((m_layerMask & (1 << collision.gameObject.layer)) != 0 )
        {
            Debug.Log(collision.gameObject.name);

            if (null == m_bounceCoroutine)
            {
                if (m_hips.velocity.magnitude > 1f)
                {
                    m_hips.velocity = m_hips.velocity * -25f;
                }
                else
                {

                    m_hips.AddForce(collision.GetContact(0).normal * m_speed * 25f);
                }

                //Vector3 bounceDirection = collider.transform.position - collision.gameObject.transform.position;
                //m_hips.AddForce(bounceDirection * m_speed * 10f);
                m_bounceCoroutine = StartCoroutine(BounceCooldown(1f));
            }
        }
    }
    private IEnumerator BounceCooldown(float bounceTime)
    {
        m_isBouncing = true;
        yield return new WaitForSeconds(bounceTime);
        m_isBouncing = false;

        m_bounceCoroutine = null;
    }

}
