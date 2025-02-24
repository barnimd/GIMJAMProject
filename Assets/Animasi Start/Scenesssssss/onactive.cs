using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class onactive : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Scene Transition Activated! Pindah scene dalam 1 detik...");
        StartCoroutine(DelayedSceneChange());
    }

    IEnumerator DelayedSceneChange()
    {
        yield return new WaitForSeconds(1f); // Delay 1 detik sebelum pindah scene
        SceneManager.LoadScene("Chapter1"); // Ganti dengan scene tujuan
    }
}
