using UnityEngine;
using UnityEngine.SceneManagement; // Perlu untuk memuat scene baru

public class TeleportScene : MonoBehaviour
{
    [SerializeField] private string targetScene; // Nama scene tujuan

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Pastikan yang masuk adalah Player
        {
            if (string.IsNullOrEmpty(targetScene))
            {
                Debug.LogError("Nama scene belum diatur! Pastikan memasukkan nama scene di Inspector.");
                return;
            }

            if (!SceneExists(targetScene))
            {
                Debug.LogError("Scene '" + targetScene + "' tidak ditemukan di Build Settings! Tambahkan di File > Build Settings.");
                return;
            }

            Debug.Log("Teleporting to scene: " + targetScene);

            // *Matikan backsound sebelum pindah scene*
            if (BacksoundManager.Instance != null)
            {
                BacksoundManager.Instance.StopBacksound();
            }

            // *Pindah ke scene tujuan*
            SceneManager.LoadScene(targetScene);
        }
    }

    private bool SceneExists(string sceneName)
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            string scenePath = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneFileName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
            if (sceneFileName == sceneName)
            {
                return true;
            }
        }
        return false;
    }
}