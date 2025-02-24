using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LigthingMadu : MonoBehaviour
{
    public Light flickerLight;
    public float minTime = 0.1f;
    public float maxTime = 1.5f;
    private Coroutine flickerRoutine;

    public bool IsLightOn => flickerLight.enabled; // Public property to check light state

    private void Start()
    {
        flickerLight.enabled = false; // Start with light OFF
    }

    public void StartFlickerMadu()
    {
        if (flickerRoutine == null)
            flickerRoutine = StartCoroutine(FlickerRoutineMadu());
    }

    public void StopFlickerMadu()
    {
        if (flickerRoutine != null)
        {
            StopCoroutine(flickerRoutine);
            flickerRoutine = null;
        }
        flickerLight.enabled = false;
    }

    IEnumerator FlickerRoutineMadu()
    {
        while (true)
        {
            flickerLight.enabled = !flickerLight.enabled;
            if(flickerLight.enabled)
            {
                AudioManagerMadu.instance.PlaySound(0);
            }
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

}
