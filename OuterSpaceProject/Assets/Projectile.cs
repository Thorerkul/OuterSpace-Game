using UnityEngine;

public class Projectile : MonoBehaviour
{
    public CombatFunctions combatFunctions;
    public PlayerScript player;
    public Vector3 direction;
    public float speed;
    public float timeToLive;
    public string type;
    public float damage;
    public float armorPenetration;

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
        transform.position += direction.normalized * speed * Time.deltaTime;

        // Check if the projectile has exceeded its time to live.
        if (Time.time - startTime >= timeToLive && isOriginal != true)
        {
            // Destroy the projectile.
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 7)
        {
            player.isHit = true;
            player.hp -= combatFunctions.damageCalculator(damage, player.defence, armorPenetration);
            player.hitTimer = player.hitCooldown;
            Destroy(gameObject);
        }

        if (other.gameObject.tag == "PlayerShield")
        {
            // Destroy the projectile.
            Destroy(gameObject);
        }

        if (other.gameObject.layer == 6)
        {
            Destroy(gameObject);
        }
    }
}