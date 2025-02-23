using UnityEngine;
using AdvancedHorrorFPS; // Tambahkan ini!

public class PlayerDoorInteraction : MonoBehaviour
{
    public Transform cameraTransform; // Kamera untuk raycast
    public float interactDistance = 3f; // Jarak interaksi
    public LayerMask doorLayer; // Layer untuk mendeteksi pintu

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out RaycastHit hit, interactDistance, doorLayer))
            {
                if (hit.collider.TryGetComponent<DoorTwoWay2>(out DoorTwoWay2 door))
                {
                    door.UseDoor(transform.position);
                }
            }
        }
    }
}
