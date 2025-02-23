using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Transform[] patrolPoints;

    public float chaseRange = 10f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [Header("Audio Settings")]
    public AudioSource footstepAudio;
    public AudioClip patrolFootstepSound;
    public AudioClip chaseFootstepSound;

    private NavMeshAgent agent;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        if (patrolPoints.Length > 0)
        {
            GoToNextPatrolPoint();
        }

        PlayFootstepSound(patrolFootstepSound);
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseRange)
        {
            Chase();
        }
        else if (isChasing)
        {
            ReturnToPatrol();
        }
        else if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            StartCoroutine(WaitAndMoveToNextPatrolPoint());
        }
    }

    void Chase()
    {
        isChasing = true;
        agent.speed = chaseSpeed;
        agent.SetDestination(player.position);
        PlayFootstepSound(chaseFootstepSound);
        


    }

    void ReturnToPatrol()
    {
        isChasing = false;
        GoToNextPatrolPoint();
        PlayFootstepSound(patrolFootstepSound);
    }

    void GoToNextPatrolPoint()
    {
        if (patrolPoints.Length == 0) return;

        agent.speed = patrolSpeed;
        agent.SetDestination(patrolPoints[currentPatrolIndex].position);
        currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
    }

    IEnumerator WaitAndMoveToNextPatrolPoint()
    {
        yield return new WaitForSeconds(0f);
        GoToNextPatrolPoint();
    }

    void PlayFootstepSound(AudioClip clip)
    {
        if (footstepAudio.clip != clip)
        {
            footstepAudio.clip = clip;
            footstepAudio.loop = true;
            footstepAudio.Play();
        }
    }
    void lari(AudioClip clip)
    {
        if (footstepAudio.clip != clip)
        {
            footstepAudio.clip = clip;
            footstepAudio.loop = true;
            footstepAudio.Play();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
