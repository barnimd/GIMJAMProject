using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    public float patrolSpeed = 2f;
    public float waypointThreshold = 0.2f; // Jarak minimum untuk berpindah waypoint

    private int currentWaypointIndex = 0;

    void Update()
    {
        if (waypoints.Length == 0) return;

        // Ambil posisi waypoint saat ini
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Gerakkan musuh ke waypoint
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, patrolSpeed * Time.deltaTime);

        // Jika sudah dekat dengan waypoint, pindah ke waypoint berikutnya
        if (Vector3.Distance(transform.position, targetWaypoint.position) < waypointThreshold)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    // GIZMOS untuk melihat waypoint di Editor
    void OnDrawGizmos()
    {
        if (waypoints.Length > 0)
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawWireSphere(waypoints[i].position, 0.3f);

                if (i < waypoints.Length - 1)
                {
                    Gizmos.DrawLine(waypoints[i].position, waypoints[i + 1].position);
                }
            }
        }
    }
}
