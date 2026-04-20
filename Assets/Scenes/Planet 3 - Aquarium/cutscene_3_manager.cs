using UnityEngine;

public class cutscene_3_manager : MonoBehaviour
{
    public Animator carAnimator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startCarAnimation();
    }

    public void startCarAnimation()
    {
        carAnimator.enabled = true;
    }
}
