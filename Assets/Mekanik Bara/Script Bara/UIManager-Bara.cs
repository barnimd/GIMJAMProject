using UnityEngine;

public class UIManagerBara : MonoBehaviour
{
    public static UIManagerBara instance;
    public GameObject deadPanel;

    void Awake()
    {
        instance = this;
    }

    public void ShowDeadPanel()
    {
        deadPanel.SetActive(true);
    }
}
