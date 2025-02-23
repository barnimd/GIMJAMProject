using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Transform holdPosition; // Posisi di tangan pemain
    private Rigidbody rb;
    private bool isHeld = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isHeld && Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }
    }

    public void PickUp(Transform hand)
    {
        isHeld = true;
        rb.isKinematic = true; // Matikan physics sepenuhnya
        rb.useGravity = false; // Matikan gravitasi agar tidak jatuh
        rb.constraints = RigidbodyConstraints.FreezeAll; // Freeze semua pergerakan
        transform.position = hand.position;
        transform.rotation = hand.rotation;
        transform.parent = hand;

        // Matikan Collider agar tidak tabrakan dengan player
        GetComponent<Collider>().enabled = false;
    }

    public void DropItem()
    {
        isHeld = false;
        rb.isKinematic = false; // Aktifkan physics
        rb.useGravity = true; // Hidupkan gravitasi
        rb.constraints = RigidbodyConstraints.None; // Unfreeze

        // Aktifkan kembali Collider agar bisa berinteraksi lagi
        GetComponent<Collider>().enabled = true;

        transform.parent = null;

        // Tambahkan dorongan ke depan
        rb.AddForce(Camera.main.transform.forward * 2f, ForceMode.Impulse);
    }



    public void StoreItem()
    {
        Destroy(gameObject); // Hancurkan barang setelah masuk ke box
    }
}
