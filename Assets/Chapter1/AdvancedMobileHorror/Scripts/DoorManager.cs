using System.Collections;
using UnityEngine;
using System.Linq;

namespace AdvancedHorrorFPS
{
    public class DoorManager : MonoBehaviour
    {
        public bool isMainDoor = false; // Tandai apakah ini pintu utama
        public bool isLocked = true; // Status terkunci untuk pintu utama
        private static int teleportCount = 0; // Menghitung jumlah teleportasi
        private static int maxTeleports = 5; // Jumlah teleportasi sebelum pintu utama terbuka

        public Vector3 teleportPosition; // Posisi tujuan teleportasi
        private Animation animation;
        public bool leftDoor = false;

        private void Start()
        {
            animation = GetComponent<Animation>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (!isMainDoor)
                {
                    TeleportPlayer(other.gameObject);
                }
                else if (isMainDoor && !isLocked)
                {
                    OpenDoor();
                }
                else
                {
                    TryToOpen();
                }
            }
        }

        private void TeleportPlayer(GameObject player)
        {
            player.transform.position = teleportPosition;
            teleportCount++;
            Debug.Log("Teleportasi ke-" + teleportCount);

            if (teleportCount >= maxTeleports)
            {
                UnlockMainDoor();
            }
        }

        private void UnlockMainDoor()
        {
            isLocked = false;
            Debug.Log("Pintu utama terbuka!");
        }

        private void OpenDoor()
        {
            if (leftDoor)
            {
                animation["Custom_Animation_DoorLeft_Open"].time = 0;
                animation["Custom_Animation_DoorLeft_Open"].speed = 1;
                animation.Play("Custom_Animation_DoorLeft_Open");
            }
            else
            {
                animation["Custom_Animation_DoorRight_Open"].time = 0;
                animation["Custom_Animation_DoorRight_Open"].speed = 1;
                animation.Play("Custom_Animation_DoorRight_Open");
            }
            Debug.Log("Pintu utama terbuka dan dapat dilewati!");
        }

        private void TryToOpen()
        {
            if (leftDoor)
            {
                animation["Custom_Animation_DoorLeft_Try"].time = 0;
                animation["Custom_Animation_DoorLeft_Try"].speed = 1;
                animation.Play("Custom_Animation_DoorLeft_Try");
            }
            else
            {
                animation["Custom_Animation_DoorRight_Try"].time = 0;
                animation["Custom_Animation_DoorRight_Try"].speed = 1;
                animation.Play("Custom_Animation_DoorRight_Try");
            }
            Debug.Log("Pintu terkunci! Selesaikan teleportasi terlebih dahulu.");
        }
    }
}
