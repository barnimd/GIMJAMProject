using AdvancedHorrorFPS;
using UnityEngine;

public class TeleportToStart : MonoBehaviour
{
    public Transform teleportDestination;
    public AudioClip startBacksound; // Backsound yang akan dimainkan saat kembali ke start
    public AudioClip specialSound; //  Suara spesial setelah 3x teleport benar

    private bool specialSoundPlayed = false; //  Agar hanya diputar sekali

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player kembali ke start, backsound dimulai lagi.");

            CharacterController controller = other.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
                other.transform.position = teleportDestination.position;
                controller.enabled = true;
            }
            else
            {
                other.transform.position = teleportDestination.position;
            }

            //  Jika player sudah melewati pintu benar 3 kali dan sound belum dimainkan
            if (TeleportTrigger.correctDoorCounter >= 3 && !specialSoundPlayed)
            {
                Debug.Log("Special sound dimainkan!");
                if (specialSound != null)
                {
                    AudioSource.PlayClipAtPoint(specialSound, transform.position);
                }
                specialSoundPlayed = true; // Tandai agar hanya dimainkan sekali
            }

            // Hidupkan kembali backsound saat kembali ke start
            if (startBacksound != null)
            {
                BacksoundManager.Instance.PlayBacksound(startBacksound);
            }
        }
    }
}
