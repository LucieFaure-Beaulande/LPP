using UnityEngine;

public class DoorPassageArielTrigger : MonoBehaviour
{
    [SerializeField] private SceneLoader sceneLoader;

    [Header("Passage porte")]
    [SerializeField] private float passageDistance = 3f;
    [SerializeField] private bool goToOtherSideForward = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Transform player = other.transform;

        Vector3 direction = goToOtherSideForward ? transform.forward : -transform.forward;

        Vector3 targetPosition = player.position + direction * passageDistance;

        CharacterController cc = player.GetComponent<CharacterController>();
        if (cc != null)
        {
            cc.enabled = false;
            player.position = targetPosition;
            cc.enabled = true;
            return;
        }

        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.position = targetPosition;
            return;
        }

        player.position = targetPosition;
    }
}