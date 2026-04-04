using UnityEngine;

public class RevealEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem smokeParticles;
    [SerializeField] private Renderer objectRenderer;
    [SerializeField] private float revealDuration = 3f;
    [SerializeField] private float delayBeforeReveal = 1f;

    private Material _material;
    private float _elapsedTime = 0f;
    private bool _isRevealing = false;
    private Color _baseColor;

    private void Start()
    {
        _material = objectRenderer.material;

        // Transparent
        _material.SetFloat("_Surface", 1f);
        _material.renderQueue = (int)UnityEngine.Rendering.RenderQueue.Transparent;

        _baseColor = _material.GetColor("_BaseColor");
        _baseColor.a = 0f;
        _material.SetColor("_BaseColor", _baseColor);

        Invoke(nameof(StartReveal), delayBeforeReveal);
    }

    private void StartReveal()
    {
        smokeParticles.Play();
        _isRevealing = true;
        _elapsedTime = 0f;
    }

    private void Update()
    {
        if (!_isRevealing) return;

        _elapsedTime += Time.deltaTime;
        float progress = Mathf.Clamp01(_elapsedTime / revealDuration);

        _baseColor.a = progress;
        _material.SetColor("_BaseColor", _baseColor);

        if (progress >= 1f)
        {
            _isRevealing = false;
            smokeParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}