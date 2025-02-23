using UnityEngine;

namespace AdvancedHorrorFPS
{
    public class MainDoorController : MonoBehaviour
    {
        private bool canOpen = false; // Pintu tidak bisa dibuka sebelum semua hint aktif

        private void Start()
        {
            // Pastikan pintu dalam keadaan tertutup di awal
            canOpen = false;
        }

        // Fungsi ini dipanggil oleh HintManager setelah semua hint aktif
        public void UnlockDoor()
        {
            canOpen = true;
            Debug.Log("Pintu utama bisa dibuka sekarang!");
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && canOpen)
            {
                OpenDoor();
            }
        }

        private void OpenDoor()
        {
            Debug.Log("Pintu utama terbuka!");
            gameObject.SetActive(false); // Nonaktifkan objek pintu (simulasi pintu terbuka)
        }
    }
}
