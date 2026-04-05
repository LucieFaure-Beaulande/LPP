using UnityEngine;

public class RevealEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private Renderer genieRenderer;
    [SerializeField] private Renderer lampRenderer;
    [SerializeField] private float revealDuration = 3f;

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

    }

    private void StartReveal()
    {
        if (smokeParticles != null)
            smokeParticles.Play();

        _isRevealing = true;
        _elapsedTime = 0f;
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
        }
    }

    public void StartRevealExternally()
    {
        if (_isRevealing) return;

        if (smokeParticles != null)
            smokeParticles.Play();

        _isRevealing = true;
        _elapsedTime = 0f;
    }
}