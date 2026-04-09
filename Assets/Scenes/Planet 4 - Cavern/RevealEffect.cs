using UnityEngine;
using UnityEngine.Events;

public class RevealEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private Renderer genieRenderer;
    [SerializeField] private Renderer lampRenderer;
    [SerializeField] private float revealDuration = 3f;
    [SerializeField] private FirstPersonController firstPersonController;

    [Header("Lights")]
    [SerializeField] private Light directionalLight3;
    [SerializeField] private Light genieSpotLight;

    [SerializeField] private Color finalSpotColor = new Color32(0x6C, 0xA2, 0xFF, 0xFF);
    [SerializeField] private float finalIntensity = 5000f;
    [SerializeField] private float finalOuterAngle = 140.8095f;

    [Header("Event")]
    public UnityEvent onRevealFinished;

    private Material _genieMaterial;
    private Material _lampMaterial;
    private float _elapsedTime = 0f;
    private bool _isRevealing = false;

    private void Start()
    {
        if (genieRenderer != null)
            _genieMaterial = genieRenderer.material;

        if (lampRenderer != null)
            _lampMaterial = lampRenderer.material;

        if (_genieMaterial != null)
            _genieMaterial.SetFloat("_Cutoff", 1f);

        if (_lampMaterial != null)
            _lampMaterial.SetFloat("_Cutoff", 0f);

        if (directionalLight3 != null)
            directionalLight3.enabled = false;
    }

    private void Update()
    {
        if (!_isRevealing) return;

        _elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(_elapsedTime / revealDuration);

        if (_genieMaterial != null)
            _genieMaterial.SetFloat("_Cutoff", 1f - progress);

        if (_lampMaterial != null)
            _lampMaterial.SetFloat("_Cutoff", progress);

        if (progress >= 1f)
        {
            if (_genieMaterial != null)
                _genieMaterial.SetFloat("_Cutoff", 0f);

            if (_lampMaterial != null)
                _lampMaterial.SetFloat("_Cutoff", 1f);

            _isRevealing = false;

            if (smokeParticles != null)
                smokeParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

            if (directionalLight3 != null)
                directionalLight3.enabled = true;

            StartCoroutine(EnhanceSpotLight());

            onRevealFinished?.Invoke();
        }
    }

    public void StartRevealExternally()
    {
        if (_isRevealing) return;

        _elapsedTime = 0f;
        _isRevealing = true;

        if (smokeParticles != null)
            smokeParticles.Play();
    }

    private System.Collections.IEnumerator EnhanceSpotLight()
    {
        if (genieSpotLight == null) yield break;

        Color startColor = genieSpotLight.color;
        float startIntensity = genieSpotLight.intensity;
        float startAngle = genieSpotLight.spotAngle;

        float duration = 1.5f;
        float time = 0f;

        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            genieSpotLight.color = Color.Lerp(startColor, finalSpotColor, t);
            genieSpotLight.intensity = Mathf.Lerp(startIntensity, finalIntensity, t);
            genieSpotLight.spotAngle = Mathf.Lerp(startAngle, finalOuterAngle, t);

            yield return null;
        }

        genieSpotLight.color = finalSpotColor;
        genieSpotLight.intensity = finalIntensity;
        genieSpotLight.spotAngle = finalOuterAngle;
    }
}