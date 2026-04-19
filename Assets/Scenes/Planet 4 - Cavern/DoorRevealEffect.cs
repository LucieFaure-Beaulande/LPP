using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DoorRevealEffect : MonoBehaviour
{
    [SerializeField] private Renderer doorRenderer;
    [SerializeField] private float revealDuration = 3f;
    [SerializeField] private ParticleSystem optionalParticles;

    [Header("Event")]
    public UnityEvent onRevealFinished;

    private Material _doorMaterial;
    private bool _isRevealing = false;

    private void Start()
    {
        if (doorRenderer == null) return;

        _doorMaterial = doorRenderer.material;

        SetMaterialToFadeMode(_doorMaterial);

        SetAlpha(0f);
    }

    private void SetMaterialToFadeMode(Material mat)
    {
        mat.SetFloat("_Mode", 2);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }

    private void SetAlpha(float alpha)
    {
        if (_doorMaterial == null) return;
        Color c = _doorMaterial.color;
        c.a = alpha;
        _doorMaterial.color = c;
    }

    public void StartReveal()
    {
        if (_isRevealing) return;
        StartCoroutine(RevealCoroutine());
    }

    private IEnumerator RevealCoroutine()
    {
        _isRevealing = true;

        if (optionalParticles != null)
            optionalParticles.Play();

        float elapsed = 0f;

        while (elapsed < revealDuration)
        {
            elapsed += Time.deltaTime;
            float progress = Mathf.Clamp01(elapsed / revealDuration);
            SetAlpha(progress);
            yield return null;
        }

        SetAlpha(1f);
        SetMaterialToOpaqueMode(_doorMaterial);

        if (optionalParticles != null)
            optionalParticles.Stop(true, ParticleSystemStopBehavior.StopEmitting);

        _isRevealing = false;
        onRevealFinished?.Invoke();
    }

    private void SetMaterialToOpaqueMode(Material mat)
    {
        mat.SetFloat("_Mode", 0);
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
        mat.SetInt("_ZWrite", 1);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.DisableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = -1;
    }
}