using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject jumpscarePanel;

    private void Start()
    {
        if (jumpscarePanel != null)
        {
            jumpscarePanel.SetActive(false); // Hide it at start
        }
    }

    public void ShowJumpscare()
    {
        if (jumpscarePanel != null)
        {
            AudioManagerMadu.instance.PlaySound(1);
            jumpscarePanel.SetActive(true);
        }
    }

    public void HideJumpscare()
    {
        if (jumpscarePanel != null)
        {
            Invoke("DisablePanel", 1f); // Wait 1 second to hide completely
        }
    }

    private void DisablePanel()
    {
        jumpscarePanel.SetActive(false);
    }

    public static implicit operator UIManager(UIManagerBara v)
    {
        throw new NotImplementedException();
    }
}
