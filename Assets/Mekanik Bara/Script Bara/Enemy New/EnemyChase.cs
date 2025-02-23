using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float chaseRange = 5f;
    public float chaseSpeed = 4f;
    public LayerMask obstacleMask; // Untuk deteksi tembok atau penghalang
    private EnemyPatrol patrolScript;
    private Rigidbody rb;

    void Start()
    {
        patrolScript = GetComponent<EnemyPatrol>();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange && CanSeePlayer())
        {
            // Mode Chase: Kejar Player
            patrolScript.enabled = false;

            Vector3 moveDirection = (player.position - transform.position).normalized;
            rb.MovePosition(transform.position + moveDirection * chaseSpeed * Time.fixedDeltaTime);
        }
        else
        {
            // Mode Patrol: Kembali ke Waypoints
            patrolScript.enabled = true;
        }
    }

    //  Mengecek apakah musuh bisa melihat player
    bool CanSeePlayer()
    {
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Raycast dari enemy ke player, kalau tidak kena obstacle, berarti terlihat
        if (!Physics.Raycast(transform.position, directionToPlayer, distanceToPlayer, obstacleMask))
        {
            return true; // Tidak ada penghalang, player terlihat
        }

        return false; // Ada penghalang, player tidak terlihat
    }

    //  GIZMOS untuk melihat chase range di Editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
