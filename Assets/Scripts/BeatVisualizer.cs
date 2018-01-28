using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatVisualizer : MonoBehaviour
{
    public Light beatLight;
    Renderer cubeRenderer;

    float lightFlashDuration = 0.1f;
    float lightFlashTime = 0f;
    float lightIntensity = 2f;

    void Start()
    {
        Music.Instance.beatDetector.OnBeat += CubeResponse;
        if (beatLight != null)
        {
            Music.Instance.beatDetector.OnBeat += LightResponse;
        }
        cubeRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        // cubeRenderer.material.color = Color.black;
    }

    void LightResponse()
    {
        StartCoroutine(FlashLight());
    }

    IEnumerator FlashLight()
    {
        float elapsedTime = 0f;
        float duration = 0.1f;
        while (elapsedTime < duration) {
            // beatLight.intensity = Mathf.Lerp(2, 3, elapsedTime / duration);
            beatLight.intensity = 2f + Mathf.PingPong(elapsedTime * 20, 1);
            beatLight.range = 9.72f + Mathf.PingPong(elapsedTime * 20, 2f);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        beatLight.intensity = 2f;
        beatLight.range = 9.72f;
    }

    void CubeResponse()
    {
        StartCoroutine(FlashWhite());
    }

    IEnumerator FlashWhite()
    {
        cubeRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        cubeRenderer.material.color = Color.black;
    }
}
