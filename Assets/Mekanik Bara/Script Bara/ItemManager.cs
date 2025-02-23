using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public GameObject[] lamps; // Drag objek lampu di inspector
    private int storedItemCount = 0;
    public int requiredItems = 5; // Jumlah barang yang dibutuhkan
    public ExitDoor exitDoor;

    public void StoreItem(GameObject item)
    {
        if (storedItemCount < requiredItems)
        {
            storedItemCount++;
            lamps[storedItemCount - 1].SetActive(true); // Nyalakan lampu sesuai jumlah item yang masuk
            Destroy(item); // Hapus item setelah disimpan

            if (storedItemCount >= requiredItems)
            {
                AudioManagerBara.Instance.levelcomplete();
                exitDoor.ActivateDoor(); // Fungsi membuka pintu jika semua barang sudah masuk
            }
        }
    }
}
