using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public enum EnemyType
{
    Melee = 0,
    Ranged = 1,
    Magic = 2,
}
//
[Serializable]
public class IkInfo
{
    public Transform transform;
    public Transform targetTransform;
    public bool isActive;
    public bool prevIsActive;
    public float currentLerp;
    public float startLerpTime;
    public float currentLerpTime;

    public int oppositeID;
    public bool oppositeActive;
}

public class EnemyBaseScript : MonoBehaviour
{
    public CombatFunctions combatFunctions;
    public EnemyType enemyType;
    public GameObject projectile;
    public LayerMask ground;
    public LayerMask obstacles;

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

    public float snapDistance;

    public List<IkInfo> IkTargets;
    public Vector2 safeRadius;
    [Range(0,1)]
    public float lerpClamp;
    //
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

                Aim();
            }
        }

        RaycastHit hit;
        // Cast a ray downwards from the GameObject's position
        if (Physics.BoxCast(transform.position + (Vector3.up * 5), new Vector3(1f, 1f, 1f), Vector3.down, out hit, Quaternion.identity, 100f, ground))
        {
            // Calculate the new position based on the hit point
            Vector3 newPosition = new Vector3(0, hit.point.y + snapDistance, 0); // Adjust the 0.5f offset if needed

            //Debug.Log(newPosition);

            // Update the GameObject's position
            transform.position = new Vector3(transform.position.x, newPosition.y, transform.position.z);
        }

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }

        IK();
    }

    void IK()
    {
        /*
        foreach (IkInfo t in IkTargets)
        {
            float distance = Vector3.Distance(t.transform.position, t.targetTransform.position);

            if (distance > safeRadius)
            {
                bool shouldactive = false;
                
                foreach (IkInfo t2 in IkTargets)
                {
                    if (t2 != t)
                    {
                        if (!t2.isActive)
                        {
                            shouldactive = true;
                            break;
                        }
                    }
                }

                t.isActive = shouldactive;

                if (t.prevIsActive != t.isActive && t.isActive == true)
                {
                    t.currentLerp = 1;
                }
            }

        if (t.isActive)
        {
                if (t.currentLerp <= 0)
                {
                    t.currentLerp = 0;
                    t.isActive = false;
                }
            RaycastHit hit;
            if (Physics.Raycast(t.transform.position + (Vector3.up * 3), Vector3.down, out hit, 100f, obstacles))
            {
                    // Move the object to the position where the raycast hit the ground
                t.transform.position = hit.point;
            }
                t.transform.position = t.targetTransform.position;
            t.currentLerp -= Time.deltaTime;
                
        }

            /*
            RaycastHit hit;
            if (Physics.Raycast(t.transform.position + (Vector3.up * 3), Vector3.down, out hit, 100f, obstacles))
            {
                // Move the object to the position where the raycast hit the ground
                t.transform.position = hit.point;
            }
        }*/

        foreach (IkInfo t in IkTargets)
        {
            Ray ray = new Ray(t.targetTransform.position + Vector3.up, -t.targetTransform.up);
            RaycastHit hit;

            // Perform the raycast and check if it hits an object on the specified layer.
            if (Physics.Raycast(ray, out hit, 100f, ground))
            {
                // Set the object's position to the hit point.
                t.targetTransform.position = hit.point;
            }

            float distance = Vector3.Distance(t.transform.position, t.targetTransform.position);

            if (distance > safeRadius.x)
            {
                t.isActive = true;
            }

            if (distance < safeRadius.y)
            {
                t.isActive = false;
            }

            t.oppositeActive = IkTargets[t.oppositeID].isActive;

            
            if (t.oppositeActive && t.isActive)
            {
                t.isActive = false;
            }

            if (!t.isActive)
            {
                t.startLerpTime = Mathf.Infinity;
                t.currentLerp = lerpClamp;
            } else
            {
                if (t.startLerpTime == Mathf.Infinity)
                {
                    t.startLerpTime = Time.timeSinceLevelLoad;
                    t.currentLerp = 1;
                }
                else if (t.startLerpTime != 0)
                {
                    t.currentLerp -= Time.deltaTime;
                    t.transform.position = Vector3.Lerp(t.targetTransform.position, t.transform.position, t.currentLerp);
                }
            }

            if (t.currentLerp < lerpClamp)
            {
                t.currentLerp = lerpClamp;
                t.isActive = false;
                t.startLerpTime = Mathf.Infinity;
            }

            t.currentLerpTime = Time.timeSinceLevelLoad;
            
        }
    }

    public void FireProjectile()
    {

        GameObject newProjectile = Instantiate(projectile, transform.position, Quaternion.identity);
        Projectile projectileScript = newProjectile.GetComponent<Projectile>();

        Vector3 fireDirection = transform.forward;

        if ( projectileScript != null)
        {
            projectileScript.direction = fireDirection;
            projectileScript.speed = 50;
            projectileScript.timeToLive = 5;
            projectileScript.type = "Test";
            projectileScript.isOriginal = false;
            projectileScript.player = player;
            projectileScript.combatFunctions = combatFunctions;
            projectileScript.damage = damage;
            projectileScript.armorPenetration = armorPenetration;
        }
    }

    public void Aim()
    {
        Transform oldt = transform;
        transform.LookAt(playerpos.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
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
            player.stamina += player.staminaRegainSpeed * player.staminaMultiplier;

            hp -= combatFunctions.damageCalculator(player.damage, defence, player.armorPenetration);

            isHit = true;
            hitTimer = hitCooldown;

            agent.enabled = false;
            rb.isKinematic = false;

            Vector3 moveDirection = rb.transform.position - player.transform.position;
            rb.AddForce(new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * player.knockback);
        }

        if (other.gameObject.tag == "PlayerShield")
        {
            hp -= combatFunctions.damageCalculator(player.damage / 4, defence, player.armorPenetration);

            isHit = true;
            hitTimer = hitCooldown;

            agent.enabled = false;
            rb.isKinematic = false;

            Vector3 moveDirection = rb.transform.position - player.transform.position;
            rb.AddForce(new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * player.knockback / 4);
        }
    }
}
