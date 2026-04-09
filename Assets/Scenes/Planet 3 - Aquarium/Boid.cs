using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    private Vector3 acceleration;

    // These are set by the Manager every frame
    [HideInInspector] public float minSpeed, maxSpeed, visualRange, separationDistance;
    public float rotationSpeed = 5f;

    public void UpdateBoid(List<Boid> neighbors, Boundary box)
    {
        Vector3 separation = Vector3.zero;
        Vector3 alignment = Vector3.zero;
        Vector3 cohesion = Vector3.zero;
        int neighborCount = 0;

        foreach (Boid other in neighbors)
        {
            if (other == this) continue;
            float dist = Vector3.Distance(transform.position, other.transform.position);

            if (dist < visualRange)
            {
                alignment += other.velocity;
                cohesion += other.transform.position;
                neighborCount++;

                if (dist < separationDistance)
                {
                    // FIX: Added +0.001f to avoid division by zero (the NaN bug)
                    separation += (transform.position - other.transform.position) / (dist + 0.001f);
                }
            }
        }

        acceleration = Vector3.zero;

        if (neighborCount > 0)
        {
            alignment /= neighborCount;
            cohesion = (cohesion / neighborCount) - transform.position;

            // Use raw vectors for internal logic, then clamp the final acceleration
            acceleration += alignment * 1.0f;
            acceleration += cohesion * 1.0f;
            acceleration += separation * 2.5f; // Keep separation slightly higher
        }

        // Reduce the boundary multiplier so it doesn't override everything
        acceleration += BoundPosition(box) * 2.0f;

        velocity += acceleration * Time.deltaTime;

        // Clamp speed
        float speed = velocity.magnitude;
        if (speed > maxSpeed) velocity = velocity.normalized * maxSpeed;
        if (speed < minSpeed) velocity = velocity.normalized * minSpeed;

        transform.position += velocity * Time.deltaTime;

        // Smooth rotation
        if (velocity.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    Vector3 BoundPosition(Boundary box)
    {
        Vector3 steer = Vector3.zero;
        float margin = 2.0f; // A smaller margin allows them to use more of the tank

        // X-Axis
        if (transform.position.x < box.min.x + margin)
            steer.x = (box.min.x + margin) - transform.position.x;
        else if (transform.position.x > box.max.x - margin)
            steer.x = (box.max.x - margin) - transform.position.x;

        // Y-Axis (Fixed steer.y)
        if (transform.position.y < box.min.y + margin)
            steer.y = (box.min.y + margin) - transform.position.y;
        else if (transform.position.y > box.max.y - margin)
            steer.y = (box.max.y - margin) - transform.position.y;

        // Z-Axis (Fixed steer.z)
        if (transform.position.z < box.min.z + margin)
            steer.z = (box.min.z + margin) - transform.position.z;
        else if (transform.position.z > box.max.z - margin)
            steer.z = (box.max.z - margin) - transform.position.z;

        return steer;
    }
}