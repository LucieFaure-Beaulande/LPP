using System.Collections;
using UnityEngine;

public class RevealTrigger : MonoBehaviour
{
    [SerializeField] private Transform playerTargetPosition;
    [SerializeField] private Transform player;
    [SerializeField] private RevealEffect revealEffect;
    [SerializeField] private FirstPersonController firstPersonController;

    [Header("Movement Easing")]
    [SerializeField] private float moveDuration = 1f;
    [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

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

            StartCoroutine(MovePlayerToTarget());
        }
    }

    private IEnumerator MovePlayerToTarget()
    {
        if (player == null || playerTargetPosition == null) yield break;

        CharacterController controller = player.GetComponent<CharacterController>();
        if (controller != null)
            controller.enabled = false;

        Vector3 lookDirection = revealEffect.transform.position - playerTargetPosition.position;
        lookDirection.y = 0f;
        Quaternion targetRotation = lookDirection != Vector3.zero
            ? Quaternion.LookRotation(lookDirection)
            : player.rotation;

        Vector3 startPosition = player.position;
        Quaternion startRotation = player.rotation;
        float elapsed = 0f;

        while (elapsed < moveDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / moveDuration);
            float eased = easeCurve.Evaluate(t);

            player.position = Vector3.Lerp(startPosition, playerTargetPosition.position, eased);
            player.rotation = Quaternion.Slerp(startRotation, targetRotation, eased);

            yield return null;
        }

        player.position = playerTargetPosition.position;
        player.rotation = targetRotation;

        if (controller != null)
            controller.enabled = true;

        revealEffect.StartRevealExternally();
    }
}