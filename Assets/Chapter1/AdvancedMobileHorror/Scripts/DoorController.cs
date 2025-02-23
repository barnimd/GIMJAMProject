using UnityEngine;

public class DoorController : MonoBehaviour
{
    public Transform teleportTarget; // Target posisi teleport
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Pastikan objek yang menyentuh adalah Player
        {
            other.transform.position = teleportTarget.position;
        }
    }
}
