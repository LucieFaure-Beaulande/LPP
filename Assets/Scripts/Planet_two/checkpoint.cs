using UnityEngine.Events;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    
    public UnityEvent<CarIdentity, checkpoint> onCheckpointEnter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as the Player
        if (collider.GetComponent<CarIdentity>().name != null)
        {
            // fire an event giving the entering gameObject and this checkpoint
            onCheckpointEnter.Invoke(collider.GetComponent<CarIdentity>(), this);
        }
    }
}
