using UnityEngine;

public class RevealTrigger : MonoBehaviour
{
    [SerializeField] private Transform playerTargetPosition;
    [SerializeField] private Transform player;
    [SerializeField] private RevealEffect revealEffect;
    private Collider _collider;
    private bool hasTriggered = false;

    private void Start()
    {
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered) return;

        if (other.CompareTag("Player"))
        {
            revealEffect.StartRevealExternally();
            hasTriggered = true;

            _collider.enabled = false;
        }
    }
}