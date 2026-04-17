using System.Collections;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Transform door;
    [SerializeField] private Transform door_hole;
    [SerializeField] private float rotateDuration = 1f;
    [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

    [Header("Objects to hide")]
    [SerializeField] private GameObject[] buttonsToHide;
    [SerializeField] private GameObject rock;

    [SerializeField] private Vector3 hingeLocalOffset = new Vector3(-0.5f, 0f, 0f);

    private bool _hasOpened = false;

    private void Start()
    {
        if (door != null)
            door.gameObject.SetActive(false);
        if (door_hole != null) 
            door_hole.gameObject.SetActive(false);
    }

    public void OpenDoorSequence()
    {
        if (door != null)
            door.gameObject.SetActive(true);
        if (door_hole != null)
            door_hole.gameObject.SetActive(true);
        if (_hasOpened) return;
        _hasOpened = true;
        StartCoroutine(DoorSequence());
    }

    private IEnumerator DoorSequence()
    {
        foreach (GameObject btn in buttonsToHide)
            if (btn != null) btn.SetActive(false);

        if (rock != null)
            rock.SetActive(false);

        yield return StartCoroutine(RotateDoorAroundHinge(90f));
    }

    private IEnumerator RotateDoorAroundHinge(float targetAngle)
    {
        Vector3 hingeWorldPos = door.TransformPoint(hingeLocalOffset);

        float elapsed = 0f;
        float startAngle = 0f;

        while (elapsed < rotateDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / rotateDuration);
            float eased = easeCurve.Evaluate(t);
            float angle = Mathf.Lerp(startAngle, targetAngle, eased);

            door.RotateAround(hingeWorldPos, Vector3.up, angle - (elapsed - Time.deltaTime < 0 ? 0 : Mathf.Lerp(startAngle, targetAngle, easeCurve.Evaluate(Mathf.Clamp01((elapsed - Time.deltaTime) / rotateDuration)))));

            yield return null;
        }

        door.RotateAround(hingeWorldPos, Vector3.up, targetAngle - Mathf.Lerp(startAngle, targetAngle, easeCurve.Evaluate(1f)));
    }
}