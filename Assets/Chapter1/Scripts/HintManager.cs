using UnityEngine;

namespace AdvancedHorrorFPS
{
    public class HintManager : MonoBehaviour
    {
        public static HintManager Instance { get; private set; }

        // Objek hint yang akan dinyalakan dalam urutan tertentu
        public GameObject hintLeft;
        public GameObject hintMiddle;
        public GameObject hintRight;

        private int activatedHints = 0;
        private const int maxHints = 3; // Maksimal hint yang bisa diaktifkan

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            // Matikan semua hint saat game dimulai
            hintLeft.SetActive(false);
            hintMiddle.SetActive(false);
            hintRight.SetActive(false);
        }

        public void ActivateHint()
        {
            if (activatedHints >= maxHints)
                return; // Mencegah aktivasi berlebihan

            switch (activatedHints)
            {
                case 0:
                    hintLeft.SetActive(true);
                    break;
                case 1:
                    hintMiddle.SetActive(true);
                    break;
                case 2:
                    hintRight.SetActive(true);
                    Debug.Log("Semua hint aktif, pintu utama bisa dibuka!");
                    OpenMainDoor();
                    break;
            }

            activatedHints++;
        }

        public bool AreAllHintsActive()
        {
            return activatedHints >= maxHints; // Perbaikan dari `hintLights.Length`
        }

        private void OpenMainDoor()
        {
            GameObject mainDoor = GameObject.FindWithTag("MainDoor"); // Cari pintu utama
            if (mainDoor != null)
            {
                MainDoorController doorController = mainDoor.GetComponent<MainDoorController>();
                if (doorController != null)
                {
                    doorController.UnlockDoor(); // Panggil fungsi untuk membuka pintu
                }
                else
                {
                    Debug.LogError("MainDoorController tidak ditemukan pada MainDoor!");
                }
            }
            else
            {
                Debug.LogError("Main door dengan tag 'MainDoor' tidak ditemukan!");
            }
        }
    }
}
