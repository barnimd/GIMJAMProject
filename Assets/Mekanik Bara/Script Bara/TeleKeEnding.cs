using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterTriggerLoadScene : MonoBehaviour
{
    [SerializeField] private string sceneToLoad; // Nama scene yang akan dimuat

    private void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek yang masuk adalah Player
        if (other.CompareTag("Player"))
        {
            // Load scene yang sudah ditentukan di Inspector
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
