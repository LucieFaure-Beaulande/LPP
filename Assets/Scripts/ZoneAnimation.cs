using UnityEngine;

public class ZoneAnimation : MonoBehaviour
{
    public Animator objetAAnimer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objetAAnimer.SetTrigger("StartAnim");
        }
    }
}
