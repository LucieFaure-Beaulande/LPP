using UnityEngine;
using System;

public class carMovement : MonoBehaviour
{
    public Vector2 input;
    public Rigidbody rg;
    public float forwardMoveSpeed;
    public float steerSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate() // Apply physics here
   {
       float speed = input.y > 0 ? forwardMoveSpeed : -forwardMoveSpeed;
       if (input.y == 0) speed = 0;
       rg.AddForce(this.transform.forward * speed, ForceMode.Acceleration);
       // Steer
       float rotation = input.x * steerSpeed * Time.fixedDeltaTime;
       transform.Rotate(0, rotation, 0, Space.World);
   }

   public void SetInputs(Vector2 input) {
        this.input.x = input.x;
        this.input.y = input.y;
   }

   public double GetSpeed() {
        Vector3 vel = rg.linearVelocity;
        return Math.Sqrt(vel.x*vel.x + vel.y*vel.y + vel.z*vel.z);
   }
}
