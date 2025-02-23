using UnityEngine;

public class ExitDoor : MonoBehaviour
{
    private DoorTwoWay doorScript;

    private void Start()
    {
        doorScript = GetComponent<DoorTwoWay>();
        if (doorScript != null)
        {
            doorScript.enabled = false; // Matikan pintu saat awal
        }
    }

    public void ActivateDoor()
    {
        if (doorScript != null)
        {
            doorScript.enabled = true; // Aktifkan pintu setelah semua item masuk
            Debug.Log("Pintu bisa digunakan sekarang!");
        }
    }
}
