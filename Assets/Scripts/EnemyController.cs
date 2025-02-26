using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    CharacterState stats;
    NavMeshAgent agent;
    Animator anim;
    public float attackRadius = 5;
    bool canAttack = true;
    float attackCooldown = 3f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        stats = GetComponent<CharacterState>();
    }

    void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);

        // Check if Player exists before accessing
        if (LevelManager.instance.Player != null)
        {
            float distance = Vector3.Distance(transform.position, LevelManager.instance.Player.position);
            if (distance < attackRadius)
            {
                agent.SetDestination(LevelManager.instance.Player.position);
                if (distance <= agent.stoppingDistance)
                {
                    if (canAttack)
                    {
                        StartCoroutine(AttackCooldown());
                        anim.SetTrigger("Attack");
                    }
                }
            }
        }
        else
        {
            // Player is destroyed, handle enemy behavior accordingly (e.g., stop chasing)
            agent.ResetPath();
        }
    }

    IEnumerator AttackCooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            stats.ChangeHealth(-other.GetComponentInParent<CharacterState>().power);
        }
    }

    public void DamagePlayer()
    {
        if (LevelManager.instance.Player != null)
        {
            LevelManager.instance.Player.GetComponent<CharacterState>().ChangeHealth(-stats.power);
        }
    }
}

