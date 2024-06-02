using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class FollowTargetRotation : MonoBehaviour
{
    [SerializeField]
    private Transform m_rotationTarget;

    [SerializeField]
    private bool m_mirrorRotation = false;

    private ConfigurableJoint m_configurableJoint;
    private void Awake()
    {
        m_configurableJoint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        m_configurableJoint.targetRotation = m_mirrorRotation ? Quaternion.Inverse(m_rotationTarget.rotation) : m_rotationTarget.rotation;
    }
}
