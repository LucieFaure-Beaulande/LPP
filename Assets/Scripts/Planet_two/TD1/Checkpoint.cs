using UnityEngine.Events;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public UnityEvent<GameObject, Checkpoint> onCheckpointEnter;

    void OnTriggerEnter(Collider collider)
    {
        // if entering object is tagged as the Player
        if (collider.gameObject.tag == "Player")
        {
            // fire an event giving the entering gameObject and this checkpoint
            onCheckpointEnter.Invoke(collider.gameObject, this);
        }

        // Or can be done by getting a component that means this is a car, like CarController
        //CarController car = collider.gameObject.GetComponent<CarController>();
        //if (car != null)
        //{
        //    onCheckpointEnter.Invoke(collider.gameObject, this);
        //}
    }
}