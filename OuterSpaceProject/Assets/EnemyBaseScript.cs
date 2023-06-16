using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBaseScript : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform playerpos;
    public PlayerScript player;

    public float damage;
    public float defence = 1;
    public float hp = 10;
    public float knockback = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(playerpos.position);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            player.isHit = true;
            player.hp -= damage / 2;
            player.hitTimer = player.hitCooldown;
            playerpos.LookAt(transform.position);
            player.rb.velocity = transform.forward * knockback;
            playerpos.rotation = Quaternion.Euler(playerpos.rotation.eulerAngles.x, 0, playerpos.rotation.eulerAngles.z);
        }
    }
}
