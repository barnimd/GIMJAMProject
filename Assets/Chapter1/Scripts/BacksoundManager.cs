using UnityEngine;

public class BacksoundManager : MonoBehaviour
{
    public static BacksoundManager Instance;
    public AudioSource audioSource;
    public AudioClip defaultBacksound; // Backsound awal saat game mulai

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.loop = true;
            audioSource.playOnAwake = false; // Jangan langsung mainkan sebelum dicek
            DontDestroyOnLoad(audioSource); // Pastikan tetap ada saat pindah scene
        }
    }

    private void Start()
    {
        // Mulai backsound saat game dimulai jika ada backsound default
        if (defaultBacksound != null)
        {
            PlayBacksound(defaultBacksound);
        }
    }

    public void PlayBacksound(AudioClip clip)
    {
        if (clip != null)
        {
            if (audioSource.isPlaying && audioSource.clip == clip) return; // Jangan restart jika lagu sudah sama

            audioSource.clip = clip;
            audioSource.Play();
            Debug.Log("Backsound dimainkan: " + clip.name);
        }
    }

    public void StopBacksound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            Debug.Log("Backsound dimatikan.");
        }
    }
}