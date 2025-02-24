using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameCanvasManager : MonoBehaviour
{
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    public Toggle audioToggle;
    public Slider sensitivitySlider;

    private bool isPaused = false;
    private PlayerMovement playerMovement; // Gunakan PlayerMovement sebagai referensi

    void Start()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(false);

        // Cari script PlayerMovement
        playerMovement = FindObjectOfType<PlayerMovement>();

        // Kunci kursor saat game dimulai
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Load pengaturan yang disimpan
        sensitivitySlider.value = PlayerPrefs.GetFloat("MouseSensitivity", 2f);
        audioToggle.isOn = PlayerPrefs.GetInt("AudioEnabled", 1) == 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsMenu.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;  // Bebaskan kursor
            Cursor.visible = true; // Tampilkan kursor

            if (playerMovement != null)
            {
                playerMovement.enabled = false; // **Matikan kontrol gerakan & kamera**
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; // Kunci kursor ke tengah layar
            Cursor.visible = false; // Sembunyikan kursor

            if (playerMovement != null)
            {
                playerMovement.enabled = true; // **Aktifkan kembali kontrol gerakan & kamera**
            }
        }
    }

    public void OpenSettings()
    {
        pauseMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
        pauseMenu.SetActive(true);

        // Simpan pengaturan
        PlayerPrefs.SetFloat("MouseSensitivity", sensitivitySlider.value);
        PlayerPrefs.SetInt("AudioEnabled", audioToggle.isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f; // Reset waktu
        Cursor.lockState = CursorLockMode.None; // Bebaskan kursor sebelum ke Main Menu
        Cursor.visible = true; // Pastikan kursor terlihat
        SceneManager.LoadScene(0);
    }
}
