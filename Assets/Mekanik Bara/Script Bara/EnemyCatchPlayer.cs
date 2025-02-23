using UnityEngine;
using UnityEngine.AI;

public class EnemyCatchPlayer : MonoBehaviour
{
    public Transform resetPoint; // Tempat musuh akan teleport setelah jumpscare
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Jika musuh menyentuh player
        {
            PlayerJumpscare playerJumpscare = other.GetComponent<PlayerJumpscare>();

            if (playerJumpscare != null)
            {
                playerJumpscare.TriggerJumpscare(); // Aktifkan jumpscare di player
            }

            // Hentikan gerakan musuh sementara
            agent.isStopped = true;
            agent.ResetPath();

            // Teleport musuh setelah 3 detik
            Invoke("TeleportEnemy", 3f);
        }
    }

    void TeleportEnemy()
    {
        if (resetPoint != null)
        {
            agent.Warp(resetPoint.position);
            agent.isStopped = false;
        }
    }
}
