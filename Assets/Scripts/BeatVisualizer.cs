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
        cubeRenderer = GetComponent<Renderer>();
    }

    void Update() {
        cubeRenderer.material.color = Color.black;
    }

    void OnBeatDetected() {
        cubeRenderer.material.color = Color.red;
    }
}
