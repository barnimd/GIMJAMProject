using System.Collections;
using UnityEngine;

public class EnemyPassDoor : MonoBehaviour
{
    public float doorCheckRadius = 1.5f; // Jarak musuh mulai menembus pintu

    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, doorCheckRadius);
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Door"))
            {
                col.isTrigger = true; // Ubah pintu jadi trigger
                StartCoroutine(ResetDoor(col)); // Reset setelah musuh lewat
            }
        }
    }

    IEnumerator ResetDoor(Collider door)
    {
        yield return new WaitForSeconds(1.5f); // Tunggu 1.5 detik sebelum normal lagi
        door.isTrigger = false; // Kembalikan collider pintu jadi solid
    }

    // Gambarkan Gizmos untuk melihat radius
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, doorCheckRadius);
    }
}
