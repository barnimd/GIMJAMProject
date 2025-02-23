using UnityEngine;

public class PlayerJumpscare : MonoBehaviour
{
    public GameObject jumpscarePanel;
    public AudioClip jumpscareAudio;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TriggerJumpscare()
    {
        if (jumpscarePanel != null)
        {
            jumpscarePanel.SetActive(true);
        }

        if (jumpscareAudio != null)
        {
            audioSource.clip = jumpscareAudio;
            audioSource.Play();
        }

        // Nonaktifkan panel setelah 3 detik
        Invoke("DisableJumpscarePanel", 3f);
    }

    void DisableJumpscarePanel()
    {
        if (jumpscarePanel != null)
        {
            jumpscarePanel.SetActive(false);
        }
    }
}
