using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public LigthingMadu flickeringLight;
    public Transform respawnPoint;
    public GameObject Player;
    public UIManager UIManager;

    private void Start()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (flickeringLight.IsLightOn)
            {
                Debug.Log("You Lose");
                StartCoroutine(Jumpscare());
            }
            else
            {
                SceneManager.LoadScene(2);
                Debug.Log("You Won");
            }
        }
    }

    IEnumerator Jumpscare()
    {
        if (UIManager != null)
        {
            UIManager.ShowJumpscare();
        }

        yield return new WaitForSeconds(1.7f); 

        Debug.Log("Respawning Player...");

        CharacterController controller = Player.GetComponent<CharacterController>();
        if (controller != null)
        {
            controller.enabled = false;
        }

        Player.transform.position = respawnPoint.position;
        Player.transform.rotation = respawnPoint.rotation;

        if (controller != null)
        {
            controller.enabled = true;
        }

        Debug.Log("Player Respawned at: " + respawnPoint.position);

        if (UIManager != null)
        {
            UIManager.HideJumpscare();
        }
    }
}