using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteractionDoor : MonoBehaviour
{
    // Start is called before the first frame update
    public LayerMask layer;
    public float raycastDistance;
    public float duration = 2;
    public bool canOpen = true;
    public Vector3 offset;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + offset, transform.forward, out hit, raycastDistance, layer))
        {
            if (!canOpen) return;
            GameObject door = hit.collider.gameObject;
            if (door.CompareTag("Door"))
            {
                // panngil script door
                door.GetComponent<DoorTwoWay>().UseDoor(transform.position);
                canOpen = false;
                Invoke(nameof(ResetCooldown), duration);
            }
        }
    }

    void ResetCooldown()
    {
        canOpen = true;
    }
}