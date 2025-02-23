using System.Collections;
using System.Linq;
using UnityEngine;

namespace AdvancedHorrorFPS
{
    public class DoorTwoWay2 : MonoBehaviour
    {
        [Header("Door Settings")]
        public float openAngle = 90f; // Sudut terbuka
        public float closeAngle = 0f; // Sudut tertutup
        public float doorSpeed = 5f; // Kecepatan pintu membuka/menutup
        public bool isLocked = false; // Apakah pintu terkunci?
        public bool giveLockMessage = true; // Tampilkan pesan jika terkunci
        public int KeyID_ToOpen = 0; // ID kunci yang diperlukan
        public bool isMainDoor = false; // Apakah ini pintu utama?

        private bool isOpen = false; // Status pintu terbuka/tutup
        private Quaternion targetRotation; // Rotasi target pintu

        private void Start()
        {
            targetRotation = Quaternion.Euler(0, closeAngle, 0); // Set rotasi awal
        }

        public void UseDoor(Vector3 playerPosition)
        {
            // Jika ini pintu utama, cek apakah semua hint sudah aktif
            if (isMainDoor)
            {
                if (!HintManager.Instance.AreAllHintsActive())
                {
                    Debug.Log("Pintu utama masih terkunci! Aktifkan semua hint dulu.");
                    GameCanvas.Instance.Show_Warning("The main door is locked. Find the right path!");
                    AudioManager.Instance.Play_Door_TryOpen();
                    return;
                }
            }

            // Jika pintu terkunci, cek apakah pemain punya kunci yang sesuai
            if (isLocked)
            {
                if (HeroPlayerScript.Instance.Hand_Key.activeSelf &&
                    HeroPlayerScript.Instance.GetCurrentKey() == KeyID_ToOpen)
                {
                    UnlockDoor();
                }
                else
                {
                    if (giveLockMessage)
                    {
                        GameCanvas.Instance.Show_Warning("Locked. Find the key!");
                    }
                    AudioManager.Instance.Play_Door_TryOpen();
                    return;
                }
            }

            // Tentukan arah buka berdasarkan posisi pemain
            Vector3 localPlayerPos = transform.InverseTransformPoint(playerPosition);
            float openDirection = localPlayerPos.x >= 0 ? openAngle : -openAngle;

            if(localPlayerPos.x >= 0.5)
            {
                openDirection = -openAngle;
            }

            if (!isOpen)
            {
                targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + openDirection, 0);
                isOpen = true;
                AudioManager.Instance.Play_Door_Wooden_Open();

                // Tutup otomatis setelah 3 detik
                Invoke(nameof(CloseDoor), 3f);
            }
        }

        public void UnlockDoor()
        {
            isLocked = false;
            HeroPlayerScript.Instance.Hand_Key.SetActive(false);
            InventoryManager.Instance.Remove_Item();
            AudioManager.Instance.Play_Door_UnLock();
            Debug.Log("Pintu berhasil dibuka!");

            // Jika ada pintu lain di dekatnya, buka juga
            GameObject nextDoor = Physics.OverlapSphere(transform.position, 4)
                .Where(x => x.GetComponent<DoorTwoWay2>() != null)
                .Where(x => x.transform != transform)
                .Select(x => x.gameObject)
                .FirstOrDefault();

            if (nextDoor != null)
            {
                nextDoor.GetComponent<DoorTwoWay2>().UnlockDoor();
            }
        }

        private void CloseDoor()
        {
            targetRotation = Quaternion.Euler(0, closeAngle, 0);
            isOpen = false;
            AudioManager.Instance.Play_Door_Close();
        }

        private void Update()
        {
            // Lerp ke rotasi target
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * doorSpeed);
        }
    }
}
