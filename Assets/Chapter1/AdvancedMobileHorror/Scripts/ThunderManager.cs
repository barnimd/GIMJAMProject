using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdvancedHorrorFPS
{
    public class ThunderManager : MonoBehaviour
    {
        public AudioClip mainMenuBGM;
        public AudioSource audioSource;

        void Start()
        {
            if (mainMenuBGM != null && audioSource != null)
            {
                audioSource.clip = mainMenuBGM;
                audioSource.loop = true; // Loop the background music
                audioSource.Play();
            }
        }
    }
}