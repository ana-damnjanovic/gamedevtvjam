using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ConfigurableJoint))]
public class FollowTargetRotation : MonoBehaviour
{
    [SerializeField]
    private Transform m_rotationTarget;

    private ConfigurableJoint m_configurableJoint;
    private void Awake()
    {
        m_configurableJoint = GetComponent<ConfigurableJoint>();
    }

    private void Update()
    {
        m_configurableJoint.targetRotation = m_rotationTarget.rotation;
    }
}
