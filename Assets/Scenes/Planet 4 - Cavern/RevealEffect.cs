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

    private void Start()
    {
        _material = objectRenderer.material;

        _material.SetFloat("_Cutoff", 1f);

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

        _material.SetFloat("_Cutoff", 1f - progress);

        if (progress >= 1f)
        {
            _material.SetFloat("_Cutoff", 0f);
            _isRevealing = false;
            smokeParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}