using UnityEngine;

public class CustomTiling : MonoBehaviour
{
    public Vector2 tiling = new Vector2(1, 1);

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();

        renderer.GetPropertyBlock(mpb);

        mpb.SetVector("_MainTex_ST", new Vector4(tiling.x, tiling.y, 0, 0));

        renderer.SetPropertyBlock(mpb);
    }
}
