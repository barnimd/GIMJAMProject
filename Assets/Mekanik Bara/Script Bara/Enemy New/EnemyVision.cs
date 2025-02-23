using UnityEngine;

public static class EnemyVision
{
    public static bool CanSeePlayer(Vector3 enemyPos, Vector3 playerPos)
    {
        RaycastHit hit;
        Vector3 direction = (playerPos - enemyPos).normalized;
        float distance = Vector3.Distance(enemyPos, playerPos);

        if (Physics.Raycast(enemyPos, direction, out hit, distance))
        {
            return hit.collider.CompareTag("Player");
        }
        return false;
    }
}
