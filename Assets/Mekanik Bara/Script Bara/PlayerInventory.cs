using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
    private ItemData[] inventoryData = new ItemData[5]; // Data Item di Slot
    public Image[] slotImages; // UI Slot
    public Transform holdPosition; // Posisi barang di tangan
    public LayerMask interactableLayer;

    private GameObject[] inventory = new GameObject[5]; // 5 Slot Inventory
    private int selectedSlot = 0; // Slot Aktif

    void Update()
    {
        HandleSlotSelection();

        if (Input.GetKeyDown(KeyCode.E)) Interact(); // Tekan E untuk ambil barang
        if (Input.GetKeyDown(KeyCode.Q)) DropItem(); // Tekan Q untuk buang barang
    }

    void HandleSlotSelection()
    {
        for (int i = 0; i < 6; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                selectedSlot = i;
                UpdateHUD();
                UpdateHeldItem();
            }
        }
    }

    void Interact()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f, interactableLayer))
        {
            if (hit.collider.CompareTag("Item")) PickUpItem(hit.collider.gameObject);
            else if (hit.collider.CompareTag("Box")) StoreItemInBox();
        }
    }

    void StoreItemInBox()
    {
        if (inventory[selectedSlot] != null) // Harus ada barang di tangan
        {
            GameObject storedItem = inventory[selectedSlot];

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f, interactableLayer))
            {
                if (hit.collider.CompareTag("Box"))
                {
                    ItemManager boxManager = hit.collider.GetComponent<ItemManager>();
                    if (boxManager != null)
                    {
                        boxManager.StoreItem(storedItem); // Simpan item ke box
                    }
                }
            }

            inventory[selectedSlot] = null; // Kosongkan slot
            Destroy(storedItem); // Hapus objek item

            Debug.Log("Barang dimasukkan ke dalam box!");

            UpdateHUD(); // Perbarui tampilan HUD
            UpdateHeldItem(); // Pastikan item di tangan juga diperbarui
        }
    }


    void PickUpItem(GameObject item)
    {
        if (inventory[selectedSlot] == null)
        {
            inventory[selectedSlot] = item;

            ItemPickup itemPickup = item.GetComponent<ItemPickup>();
            if (itemPickup != null && itemPickup.itemData != null)
            {
                inventoryData[selectedSlot] = itemPickup.itemData;
                slotImages[selectedSlot].sprite = itemPickup.itemData.itemIcon; // Set sprite UI
                slotImages[selectedSlot].color = Color.white; // Pastikan terlihat
            }

            Rigidbody rb = item.GetComponent<Rigidbody>();
            Collider col = item.GetComponent<Collider>();

            if (rb != null)
            {
                rb.isKinematic = true;
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            if (col != null) col.isTrigger = true;

            item.transform.SetParent(holdPosition);
            item.transform.localPosition = Vector3.zero;
            item.transform.localRotation = Quaternion.identity;

            item.SetActive(true);
            Debug.Log($"Item masuk ke slot {selectedSlot + 1}");

            UpdateHUD();
            UpdateHeldItem();
        }
    }

    void DropItem()
    {
        if (inventory[selectedSlot] != null)
        {
            GameObject droppedItem = inventory[selectedSlot];
            inventory[selectedSlot] = null; // Kosongkan slot
            inventoryData[selectedSlot] = null; // Kosongkan data item

            droppedItem.transform.SetParent(null);
            droppedItem.transform.position = transform.position + transform.forward * 1f;

            Rigidbody rb = droppedItem.GetComponent<Rigidbody>();
            Collider col = droppedItem.GetComponent<Collider>();

            if (rb != null)
            {
                rb.isKinematic = false;
                rb.AddForce(transform.forward * 2f, ForceMode.Impulse);
            }

            if (col != null) col.isTrigger = false;

            // **Reset HUD untuk slot ini**
            slotImages[selectedSlot].sprite = null;
            slotImages[selectedSlot].color = new Color(1, 1, 1, 0); // Transparan

            Debug.Log($"Item dari slot {selectedSlot + 1} dibuang");

            UpdateHUD();
            UpdateHeldItem(); // Pastikan item yang dipegang juga diperbarui
        }
    }


    void UpdateHUD()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (inventory[i] != null && inventoryData[i] != null)
            {
                slotImages[i].sprite = inventoryData[i].itemIcon;
                slotImages[i].color = Color.white;
                slotImages[i].enabled = true;
            }
            else
            {
                slotImages[i].sprite = null;
                slotImages[i].color = new Color(1, 1, 1, 0);
                slotImages[i].enabled = false;
            }

            // Update highlight
            Transform highlight = slotImages[i].transform.Find("Highlight");
            if (highlight != null) highlight.gameObject.SetActive(i == selectedSlot);
        }
    }



    void UpdateHeldItem()
    {
        foreach (GameObject item in inventory)
        {
            if (item != null) item.SetActive(false);
        }

        if (inventory[selectedSlot] != null)
        {
            inventory[selectedSlot].SetActive(true);
            inventory[selectedSlot].transform.position = holdPosition.position;
            inventory[selectedSlot].transform.parent = holdPosition;
        }
    }

}
