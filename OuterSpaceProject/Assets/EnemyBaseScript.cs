using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class EnemyBaseScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Rigidbody rb;
    public Transform playerpos;
    public PlayerScript player;

    public float damage;
    public float defence = 1;
    public float hp = 10;
    public float knockback = 10;

    public bool isHit;
    public float hitCooldown;
    public float hitTimer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
            if (hp < 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            player.isHit = true;
            player.hp -= damage / 2;
            player.hitTimer = player.hitCooldown;
            /*
            playerpos.LookAt(transform.position);
            playerpos.rotation = Quaternion.Euler(playerpos.rotation.eulerAngles.x, 0, playerpos.rotation.eulerAngles.z);
            player.rb.velocity = transform.forward * knockback;
            */

            Vector3 moveDirection = player.rb.transform.position - transform.position;
            player.rb.AddForce(new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * knockback);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 9)
        {
            hp -= player.damage / 2;
            /*
            transform.LookAt(playerpos.position);
            transform.rotation = Quaternion.Euler(0, transform.rotation.y, 0);
            rb.velocity = transform.forward * knockback;
            */

            isHit = true;
            hitTimer = hitCooldown;

            agent.enabled = false;
            rb.isKinematic = false;

            Vector3 moveDirection = rb.transform.position - player.transform.position;
            rb.AddForce(new Vector3(moveDirection.normalized.x, 0, moveDirection.normalized.z) * player.knockback);

            Debug.Log(rb.velocity);
            Debug.Log(playerpos.forward * player.knockback);
            Debug.Log(transform.rotation.eulerAngles);
        }
    }
}
