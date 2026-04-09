using UnityEngine;
using System.Collections.Generic;

public class BoidManager : MonoBehaviour
{
    [Header("Settings")]
    public GameObject boidPrefab;
    public int spawnCount = 30;
    public Vector3 aquariumSize = new Vector3(20, 10, 20);

    [Header("Boid Physics Sliders")]
    [Range(1f, 10f)] public float minSpeed = 2f;
    [Range(1f, 20f)] public float maxSpeed = 5f;
    [Range(1f, 15f)] public float visualRange = 5f;
    [Range(0.5f, 5f)] public float separationDist = 1.5f;

    private List<Boid> allBoids = new List<Boid>();
    private Boundary aquarium;

    void Start()
    {
        // Setup boundary once for spawn
        UpdateBoundary();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 randomPos = new Vector3(
                Random.Range(aquarium.min.x, aquarium.max.x),
                Random.Range(aquarium.min.y, aquarium.max.y),
                Random.Range(aquarium.min.z, aquarium.max.z)
            );

            GameObject go = Instantiate(boidPrefab, randomPos, Quaternion.identity);
            Boid boid = go.GetComponent<Boid>();
            boid.velocity = Random.insideUnitSphere * maxSpeed;
            allBoids.Add(boid);
        }
    }

    void Update()
    {
        UpdateBoundary();

        foreach (Boid boid in allBoids)
        {
            // CRITICAL: Pass the slider values to the Boids every frame!
            boid.minSpeed = minSpeed;
            boid.maxSpeed = maxSpeed;
            boid.visualRange = visualRange;
            boid.separationDistance = separationDist;

            boid.UpdateBoid(allBoids, aquarium);
        }
    }

    void UpdateBoundary()
    {
        aquarium.min = transform.position - (aquariumSize / 2);
        aquarium.max = transform.position + (aquariumSize / 2);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(transform.position, aquariumSize);
    }
}

[System.Serializable]
public struct Boundary
{
    public Vector3 min;
    public Vector3 max;

    // A helper to visualize the box in the Unity Editor
    public Vector3 Center => (min + max) / 2;
    public Vector3 Size => max - min;
}