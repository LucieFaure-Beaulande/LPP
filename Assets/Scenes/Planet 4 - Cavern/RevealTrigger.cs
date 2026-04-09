using UnityEngine;

public class RevealTrigger : MonoBehaviour
{
    [SerializeField] private Transform playerTargetPosition;
    [SerializeField] private Transform player;
    [SerializeField] private RevealEffect revealEffect;
    [SerializeField] private FirstPersonController firstPersonController;

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
            hasTriggered = true;
            _collider.enabled = false;

            if (firstPersonController != null)
                firstPersonController.DisablePlayerControl();

            MovePlayerToTarget();

            revealEffect.StartRevealExternally();
        }
    }

    private void MovePlayerToTarget()
    {
        if (player == null || playerTargetPosition == null) return;

        CharacterController controller = player.GetComponent<CharacterController>();

        if (controller != null)
            controller.enabled = false;

        player.position = playerTargetPosition.position;

        Vector3 lookDirection = revealEffect.transform.position - player.position;
        lookDirection.y = 0f;

        if (lookDirection != Vector3.zero)
            player.rotation = Quaternion.LookRotation(lookDirection);

        if (controller != null)
            controller.enabled = true;
    }
}