using System.Collections;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Transform door;
    [SerializeField] private Transform door_hole;
    [SerializeField] private float rotateDuration = 1f;
    [SerializeField] private AnimationCurve easeCurve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private Color smokeColor = Color.white;
    [SerializeField] private Gradient smokeGradient;

    [Header("Reveal Effect")]
    [SerializeField] private float revealDuration = 2f;

    [Header("Objects to hide")]
    [SerializeField] private GameObject[] buttonsToHide;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject genie;
    [SerializeField] private Vector3 hingeLocalOffset = new Vector3(-0.5f, 0f, 0f);

    private bool _hasOpened = false;
    private Material[] _doorMaterials;
    private Material[] _doorHoleMaterials;

    private void Start()
    {
        if (door != null)
            door.gameObject.SetActive(false);
        if (door_hole != null)
            door_hole.gameObject.SetActive(false);
    }

    public void OpenDoorSequence(string btnName)
    {
        if (_hasOpened) return;
        _hasOpened = true;

        if (door != null)
        {
            Renderer[] renderers = door.GetComponentsInChildren<Renderer>(true); 
            var matList = new System.Collections.Generic.List<Material>();
            foreach (Renderer r in renderers)
                foreach (Material m in r.materials)
                    matList.Add(m);
            _doorMaterials = matList.ToArray();

            foreach (Material m in _doorMaterials)
                m.SetFloat("_Cutoff", 1f);

            door.gameObject.SetActive(true);
        }
        if (door_hole != null)
        {
            Renderer[] renderers = door_hole.GetComponentsInChildren<Renderer>(true);
            var matList = new System.Collections.Generic.List<Material>();
            foreach (Renderer r in renderers)
                foreach (Material m in r.materials)
                    matList.Add(m);
            _doorHoleMaterials = matList.ToArray();

            foreach (Material m in _doorHoleMaterials)
                m.SetFloat("_Cutoff", 1f);

            door_hole.gameObject.SetActive(true);
        }

        StartCoroutine(DoorSequence());
        DoorPassageTrigger.setBtnName(btnName);
    }

    private IEnumerator DoorSequence()
    {
        if (smokeParticles != null)
        {
            var main = smokeParticles.main;
            main.startColor = smokeColor;
            var colorOverLifetime = smokeParticles.colorOverLifetime;
            colorOverLifetime.enabled = true;
            colorOverLifetime.color = new ParticleSystem.MinMaxGradient(smokeGradient);
            smokeParticles.Play();
        }

        foreach (GameObject btn in buttonsToHide)
            if (btn != null) btn.SetActive(false);

        if (rock != null) rock.SetActive(false);
        if (genie != null) genie.SetActive(false);

        yield return StartCoroutine(RevealDoor());
        if (smokeParticles != null)
        {
            smokeParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            yield return new WaitWhile(() => smokeParticles.particleCount > 0);
        }
        yield return StartCoroutine(RotateDoorAroundHinge(90f));

        if (smokeParticles != null)
            smokeParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
    }

    private IEnumerator RevealDoor()
    {
        float elapsed = 0f;

        while (elapsed < revealDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / revealDuration);

            foreach (Material m in _doorMaterials)
                m.SetFloat("_Cutoff", 1f - progress);
            foreach (Material m in _doorHoleMaterials)
                m.SetFloat("_Cutoff", 1f - progress);

            yield return null;
        }

        foreach (Material m in _doorMaterials)
            m.SetFloat("_Cutoff", 0f);
        foreach (Material m in _doorHoleMaterials)
            m.SetFloat("_Cutoff", 0f);
    }

    public IEnumerator RotateDoorAroundHinge(float targetAngle)
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
            float prevEased = easeCurve.Evaluate(Mathf.Clamp01((elapsed - Time.deltaTime) / rotateDuration));
            float prevAngle = elapsed - Time.deltaTime < 0 ? 0f : Mathf.Lerp(startAngle, targetAngle, prevEased);
            door.RotateAround(hingeWorldPos, Vector3.up, angle - prevAngle);
            yield return null;
        }
    }
}