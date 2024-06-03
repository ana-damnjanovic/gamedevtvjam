using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLostDetector : MonoBehaviour
{
    public event System.Action<RagdollPlayerController> PlayerLost = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<RagdollPlayerController>(out RagdollPlayerController playerController))
        {
            PlayerLost.Invoke(playerController);
        }
    }
}
