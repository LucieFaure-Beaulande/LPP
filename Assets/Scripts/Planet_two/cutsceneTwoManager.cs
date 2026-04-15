using UnityEngine;

public class cutsceneTwoManager : MonoBehaviour
{
    
    public Animator doorAnimator;
    public Animator car1Animator;
    public Animator car2Animator;
    public Animator car3Animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        startDoorAnimation();
    }

    public void startDoorAnimation() {
        doorAnimator.enabled = true;
        car1Animator.enabled = false;
        car2Animator.enabled = false;
        car3Animator.enabled = false;
    }
    
    public void startCarAnimations() {
        doorAnimator.enabled = false;
        car1Animator.enabled = true;
        car2Animator.enabled = true;
        car3Animator.enabled = true;
    }
}
