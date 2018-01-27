using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeatVisualizer : MonoBehaviour
{
    Color currentColor;
    Renderer cubeRenderer;

    void Start()
    {
        Music.Instance.beatDetector.OnBeat += OnBeatDetected;
        Music.Instance.Play();
        currentColor = Color.black;
        cubeRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
        // cubeRenderer.material.color = Color.black;
    }

    void OnBeatDetected() {
        StartCoroutine(FlashWhite());
    }

    IEnumerator FlashWhite() {
        cubeRenderer.material.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        cubeRenderer.material.color = Color.black;
    }
}
