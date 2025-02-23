using UnityEngine;

public class AudioManagerBara : MonoBehaviour
{
    public static AudioManagerBara Instance;

    public AudioSource sfxSource;
    public AudioClip footstepSound;
    public AudioClip doorOpenSound;
    public AudioClip itemPickupSound;
    public AudioClip itemDropSound;
    public AudioClip jumppingloncat;
    public AudioClip pintukebuka;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayFootstep()
    {
        if (footstepSound != null)
        {
            sfxSource.PlayOneShot(footstepSound);
        }
    }
    public void bajingloncat()
    {
        if (doorOpenSound != null)
        {
            sfxSource.PlayOneShot(jumppingloncat);
        }
    }
    public void levelcomplete()
    {
        if (doorOpenSound != null)
        {
            sfxSource.PlayOneShot(pintukebuka);
        }
    }
    public void PlayDoorOpen()
    {
        if (doorOpenSound != null)
        {
            sfxSource.PlayOneShot(doorOpenSound);
        }
    }

    public void PlayItemPickup()
    {
        if (itemPickupSound != null)
        {
            sfxSource.PlayOneShot(itemPickupSound);
        }
    }

    public void PlayItemDrop()
    {
        if (itemDropSound != null)
        {
            sfxSource.PlayOneShot(itemDropSound);
        }
    }
}
