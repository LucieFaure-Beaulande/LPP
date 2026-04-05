using UnityEngine;

public class RevealTrigger : MonoBehaviour
{
    [SerializeField] private RevealEffect revealEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            revealEffect.StartRevealExternally();
        }
    }
}