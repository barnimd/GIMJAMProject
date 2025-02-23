using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerMadu : MonoBehaviour
{
    public static AudioManagerMadu instance;
    AudioSource audioSource;
    [SerializeField] public AudioClip[] audioClips;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(int clipIndex)
    {
        audioSource.PlayOneShot(audioClips[clipIndex]);
    }

}
