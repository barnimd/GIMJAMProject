using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    public Transform teleportTarget; // Objek tujuan teleport

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy")) // Jika menyentuh musuh
        {
            if (teleportTarget != null)
            {
                transform.position = teleportTarget.position; // Teleport ke posisi target
                Debug.Log("Player terkena musuh! Teleport ke lokasi aman.");
            }
            else
            {
                Debug.LogWarning("Teleport target belum diatur!");
            }
        }
    }
}
