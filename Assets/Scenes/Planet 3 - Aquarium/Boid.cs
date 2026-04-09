using UnityEngine;
using System.Collections.Generic;

public class Boid : MonoBehaviour
{
    public Vector3 velocity;
    private Vector3 acceleration;

    public Animator animator;

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

            acceleration += alignment * 1.0f;
            acceleration += cohesion * 1.0f;
            acceleration += separation * 2.5f;
        }

        // 1. CLAMP THE FLOCKING FORCES
        // This stops them from instantly snapping 180 degrees. It forces an arc.
        acceleration = Vector3.ClampMagnitude(acceleration, maxSpeed);

        // 2. ADD THE BOUNDARY SPRING
        // We add this AFTER clamping so the walls always overpower the flocking.
        acceleration += BoundPosition(box);

        velocity += acceleration * Time.deltaTime;

        // Clamp speed
        float speed = velocity.magnitude;
        // Failsafe to prevent zero-vector locking
        if (speed < 0.01f) velocity = transform.forward * minSpeed;
        else if (speed > maxSpeed) velocity = (velocity / speed) * maxSpeed;
        else if (speed < minSpeed) velocity = (velocity / speed) * minSpeed;

        if (animator != null)
        {
            // Option A: Send the speed to the Animator parameter for transitions/blend trees
            animator.SetFloat("Speed", speed);

            // Option B: Scale the playback speed so they flap/swim faster when moving fast
            // This maps their current speed relative to their max speed (e.g., 0.5 to 1.0)
            animator.speed = Mathf.Clamp(speed / maxSpeed, 0.5f, 1.5f);
        }

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
        Vector3 force = Vector3.zero;

        // INCREASE THE MARGIN! 
        // A smooth turn requires physical space. If maxSpeed is 5, they need at least 6 units to curve.
        float margin = 6.0f;

        // PROGRESSIVE SPRING LOGIC
        // Instead of a flat toggle, the force scales up the closer they get to the absolute edge.

        // X-Axis
        if (transform.position.x < box.min.x + margin)
        {
            force.x = (box.min.x + margin) - transform.position.x;
        }
        else if (transform.position.x > box.max.x - margin)
        {
            force.x = (box.max.x - margin) - transform.position.x;
        }

        // Y-Axis
        if (transform.position.y < box.min.y + margin)
        {
            force.y = (box.min.y + margin) - transform.position.y;
        }
        else if (transform.position.y > box.max.y - margin)
        {
            force.y = (box.max.y - margin) - transform.position.y;
        }

        // Z-Axis
        if (transform.position.z < box.min.z + margin)
        {
            force.z = (box.min.z + margin) - transform.position.z;
        }
        else if (transform.position.z > box.max.z - margin)
        {
            force.z = (box.max.z - margin) - transform.position.z;
        }

        // Multiply by a tuning factor to make the spring firm enough to stop fast fish.
        return force * 3.0f;
    }
}