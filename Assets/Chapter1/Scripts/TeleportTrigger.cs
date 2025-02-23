using UnityEngine;
using System.Collections;

namespace AdvancedHorrorFPS
{
    public class TeleportTrigger : MonoBehaviour
    {
        public Transform teleportDestination;
        public bool isCorrectDoor = false;
        private bool hasBeenUsed = false;

        public AudioClip teleportSound;
        public AudioClip correctDoorSound;
        public float soundDelay = 1.0f;

        private AudioSource audioSource;

        //  Tambahkan counter statis untuk melacak jumlah pintu benar yang dilewati
        public static int correctDoorCounter = 0;

        private void Start()
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Debug.Log("Player masuk ke teleport, backsound dimatikan.");

                // Matikan backsound saat teleport
                BacksoundManager.Instance.StopBacksound();

                // Teleport Player
                FirstPersonController.Instance.Teleport(teleportDestination.position);

                // Mainkan suara teleport (langsung)
                if (teleportSound != null)
                {
                    audioSource.PlayOneShot(teleportSound);
                }

                // Jika ini pintu benar dan belum pernah dipakai, aktifkan hint dan mainkan suara khusus
                if (isCorrectDoor && !hasBeenUsed)
                {
                    HintManager.Instance.ActivateHint();
                    isCorrectDoor = false;
                    hasBeenUsed = true;

                    //  Tambahkan counter ketika pintu benar dilewati
                    correctDoorCounter++;

                    // Mainkan correctDoorSound dengan delay
                    StartCoroutine(PlayCorrectDoorSound());
                }
                else
                {
                    hasBeenUsed = true;
                }
            }
        }

        private IEnumerator PlayCorrectDoorSound()
        {
            yield return new WaitForSeconds(soundDelay);
            if (correctDoorSound != null)
            {
                audioSource.PlayOneShot(correctDoorSound);
            }
        }
    }
}
