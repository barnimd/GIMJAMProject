using UnityEngine;

public class GlowEffect : MonoBehaviour
{
    public Material glowMaterial;  // Material efek glow
    public float glowIntensity = 2f; // Intensitas Emission saat berkedip
    public float blinkInterval = 1f; // Waktu antar kilauan

    private Material originalMaterial;
    private Renderer objRenderer;
    private bool isGlowing = false;
    private float timer;

    void Start()
    {
        objRenderer = GetComponent<Renderer>();

        if (objRenderer != null)
        {
            originalMaterial = objRenderer.material; // Simpan material asli
        }
        else
        {
            Debug.LogError("GlowEffect ERROR: Tidak ada Renderer di " + gameObject.name);
        }
    }

    void Update()
    {
        if (objRenderer == null) return; // Hindari error jika tidak ada Renderer

        timer += Time.deltaTime;
        if (timer >= blinkInterval)
        {
            timer = 0f;
            ToggleGlow();
        }
    }

    void ToggleGlow()
    {
        if (objRenderer == null) return; // Pastikan ada Renderer

        isGlowing = !isGlowing;

        if (isGlowing)
        {
            objRenderer.material = glowMaterial;
            objRenderer.material.EnableKeyword("_EMISSION");
            objRenderer.material.SetColor("_EmissionColor", Color.white * glowIntensity);
        }
        else
        {
            objRenderer.material = originalMaterial;
        }
    }
}
