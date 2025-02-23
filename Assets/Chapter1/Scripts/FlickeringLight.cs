using System.Collections;
using UnityEngine;

public class FlickeringLight : MonoBehaviour
{
    public Light flickeringLight;
    public float minTime = 0.1f;
    public float maxTime = 0.5f;

    private void Start()
    {
        StartCoroutine(Flicker());
    }

    private IEnumerator Flicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            flickeringLight.enabled = !flickeringLight.enabled;
        }
    }
}
