using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Quaternion direction;
    public float speed;
    public float timeToLive;
    public string type;

    private float startTime;

    public bool isOriginal = false;

    private void Start()
    {
        // Record the start time to calculate the time the projectile has existed.
        startTime = Time.time;
    }

    private void Update()
    {
        // Move the projectile in the specified direction at the given speed.
        transform.position += direction.eulerAngles * speed * Time.deltaTime;

        // Check if the projectile has exceeded its time to live.
        if (Time.time - startTime >= timeToLive && isOriginal != true)
        {
            // Destroy the projectile.
            Destroy(gameObject);
        }
    }
}