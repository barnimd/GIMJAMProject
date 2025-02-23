using UnityEngine;

public class DoorTwoWay: MonoBehaviour
{
    public float openAngle = 90f; // Sudut pintu terbuka
    public float closeAngle = 0f; // Sudut pintu tertutup
    public float doorSpeed = 5f; // Kecepatan membuka/menutup pintu

    private bool isOpen = false; // Status apakah pintu terbuka
    private Quaternion targetRotation; // Rotasi target

    private void Start()
    {
        targetRotation = Quaternion.Euler(0, closeAngle, 0); // Set rotasi awal
    }

    public void UseDoor(Vector3 playerPosition)
    {
        if (!isOpen)
        {
            // Tentukan apakah pintu terbuka ke kiri atau kanan berdasarkan posisi pemain
            float dot = Vector3.Dot(transform.forward, (playerPosition - transform.position).normalized);
            float openDirection = dot >= 0 ? openAngle : -openAngle;

            // Tentukan rotasi target
            targetRotation = Quaternion.Euler(0, openDirection, 0);
            isOpen = true;

            // Tutup pintu otomatis setelah beberapa detik
            Invoke(nameof(CloseDoor), 1.5f);
        }
    }

    private void CloseDoor()
    {
        targetRotation = Quaternion.Euler(0, closeAngle, 0);
        isOpen = false;
    }

    private void Update()
    {
        // Lerp ke rotasi target
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * doorSpeed);
    }
}
