using UnityEngine;

public class PlayerDoorInteractionBara : MonoBehaviour
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
                if (hit.collider.TryGetComponent<DoorTwoWay>(out DoorTwoWay door))
                {
                    door.UseDoor(transform.position);
                    AudioManagerBara.Instance.PlayDoorOpen();
                }
            }
        }
    }
}
