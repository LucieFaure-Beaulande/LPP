using System.Collections;
using UnityEngine;

public class DoorManager_Ariel : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Transform door;
    [SerializeField] private Transform door_hole;
    [SerializeField] private float rotateDuration = 1f;
    [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private Vector3 hingeLocalOffset = new Vector3(-0.5f, 0f, 0f);

    [Header("Trigger")]
    [SerializeField] private string playerTag = "Player";

    private bool _hasOpened = false;
    private bool _isOpening = false;

    private Material[] _doorMaterials;
    private Material[] _doorHoleMaterials;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        OpenDoorArielSequence();
    }

    public void OpenDoorArielSequence()
    {
        if (_hasOpened || _isOpening) return;

        StartCoroutine(DoorArielSequence());
    }

    private IEnumerator DoorArielSequence()
    {
        _isOpening = true;

        yield return StartCoroutine(RotateDoorArielAroundHinge(90f));

        _hasOpened = true;
        _isOpening = false;
    }

    private IEnumerator RotateDoorArielAroundHinge(float targetAngle)
    {
        if (door == null) yield break;

        Vector3 hingeWorldPos = door.TransformPoint(hingeLocalOffset);
        float elapsed = 0f;
        float startAngle = 0f;

        while (elapsed < rotateDuration)
        {
            elapsed += Time.deltaTime;

            float t = Mathf.Clamp01(elapsed / rotateDuration);
            float eased = easeCurve.Evaluate(t);
            float angle = Mathf.Lerp(startAngle, targetAngle, eased);

            float prevT = Mathf.Clamp01((elapsed - Time.deltaTime) / rotateDuration);
            float prevEased = easeCurve.Evaluate(prevT);
            float prevAngle = Mathf.Lerp(startAngle, targetAngle, prevEased);

            door.RotateAround(hingeWorldPos, Vector3.up, angle - prevAngle);

            yield return null;
        }
    }
}