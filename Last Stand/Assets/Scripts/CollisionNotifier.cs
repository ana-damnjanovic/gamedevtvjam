using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CollisionNotifier : MonoBehaviour
{
    public event System.Action<Collider, Collision> Collided;

    private Collider m_collider;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
    }

    private void OnCollisionEnter(Collision other)
    {
        Collided.Invoke(m_collider, other);
    }
}
