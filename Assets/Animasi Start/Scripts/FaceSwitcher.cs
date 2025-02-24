using UnityEngine;
using TMPro;
using System.Collections;


public class FaceSwitcher : MonoBehaviour
{
    public SkinnedMeshRenderer faceRenderer;
    public Mesh newFaceMesh;

    // Create fields for all materials
    public Material material1; // Material.001
    public Material material2; // Material.003
    public Material material3; // Material
    public Material material4; // Material.006
    public Material material5; // Material.002
    public Material material6; // Material.004
    public Material material7; // Material.005

    // Monologue UI Elements
    public GameObject dialogPanel;
    public TMP_Text dialogText;
    public string[] monologueLines;
    private int currentLineIndex = 0;


    public void ChangeFace()
    {
        if (faceRenderer != null && newFaceMesh != null)
        {
            // Change the face mesh
            faceRenderer.sharedMesh = newFaceMesh;

            // Create new materials array
            Material[] materials = new Material[7];

            // Assign materials in the correct order
            materials[0] = material1; // Material.001
            materials[1] = material2; // Material.003
            materials[2] = material3; // Material
            materials[3] = material4; // Material.006
            materials[4] = material5; // Material.002
            materials[5] = material6; // Material.004
            materials[6] = material7; // Material.005

            // Apply all materials at once
            faceRenderer.materials = materials;
        }
    }

    public void StartMonologue()
    {
        if (monologueLines.Length > 0)
        {
            dialogPanel.SetActive(true);
            currentLineIndex = 0;
            StartCoroutine(ShowMonologue());
        }
    }

    private IEnumerator ShowMonologue()
    {
        while (currentLineIndex < monologueLines.Length)
        {
            dialogText.text = monologueLines[currentLineIndex];
            yield return new WaitForSeconds(2.5f);
            currentLineIndex++;
        }

        yield return new WaitForSeconds(1f); 
        dialogPanel.SetActive(false);
    }
}
