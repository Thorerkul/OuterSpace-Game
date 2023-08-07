using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyBaseScript : MonoBehaviour
{
    public CombatFunctions combatFunctions;

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
            agent.SetDestination(playerpos.position);
        }

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
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
