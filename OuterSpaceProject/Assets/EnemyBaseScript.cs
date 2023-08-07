using System.Collections;
using System.Collections.Generic;
using System.IO.Pipes;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using static UnityEditor.Rendering.CameraUI;

public enum EnemyType
{
    Melee = 0,
    Ranged = 1,
    Magic = 2,
}

public class EnemyBaseScript : MonoBehaviour
{
    public CombatFunctions combatFunctions;
    public EnemyType enemyType;
    public GameObject projectile;

    public NavMeshAgent agent;
    public Rigidbody rb;
    public Transform playerpos;
    public PlayerScript player;

    public float damage;
    public float defence = 1;
    public float armorPenetration = 0;
    public float hp = 10;
    public float knockback = 10;

    public bool isHit;
    public float hitCooldown;
    public float hitTimer;

    public float projectileSpawnTime;
    public float projectileSpawnTimer;

    // Update is called once per frame
    void Update()
    {
        if (isHit)
        {
            hitTimer -= Time.deltaTime;
            if (hitTimer < 0)
            {
                isHit = false;
                agent.enabled = true;
                rb.isKinematic = true;
            }
        } else
        {
            if (enemyType == EnemyType.Melee)
            {
                agent.SetDestination(playerpos.position);
            } else if (enemyType == EnemyType.Ranged)
            {
                agent.SetDestination(playerpos.position + new Vector3(0, 0, 10));
                if (projectileSpawnTimer <= 0)
                {
                    projectileSpawnTimer = projectileSpawnTime;

                    FireProjectile();
                }
                projectileSpawnTimer -= Time.deltaTime;
            }
        }

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    public void FireProjectile()
    {
        Transform oldtrans = transform;

        transform.LookAt(player.transform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);

        // Get the direction the shooter is facing (forward).
        Vector3 fireDirection = transform.forward;

        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        if (projectileScript != null)
        {
            projectileScript.direction = fireDirection;
            projectileScript.speed = -100;
            projectileScript.timeToLive = 5;
            projectileScript.type = "Test";
            projectileScript.isOriginal = false;
            projectileScript.player = player;
            projectileScript.combatFunctions = combatFunctions;
            projectileScript.damage = damage;
            projectileScript.armorPenetration = armorPenetration;
        }

        transform.rotation = oldtrans.rotation;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            player.isHit = true;
            player.hp -= combatFunctions.damageCalculator(damage, player.defence, armorPenetration);
            player.hitTimer = player.hitCooldown;

            isHit = true;
            hitTimer = hitCooldown;

            agent.enabled = false;
            rb.isKinematic = false;

            Vector3 moveDirection = rb.transform.position - player.transform.position;
            rb.AddForce(new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * player.knockback);

            //Vector3 moveDirection = player.rb.transform.position - transform.position;
            //player.rb.AddForce(new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * knockback);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9 && player.isSwinging)
        {
            hp -= combatFunctions.damageCalculator(player.damage, defence, player.armorPenetration);

            isHit = true;
            hitTimer = hitCooldown;

            agent.enabled = false;
            rb.isKinematic = false;

            Vector3 moveDirection = rb.transform.position - player.transform.position;
            rb.AddForce(new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * player.knockback);
        }
    }
}
