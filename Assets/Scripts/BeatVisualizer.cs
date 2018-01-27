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
        currentColor = Color.Lerp(currentColor, Color.black, .01f);
        cubeRenderer.material.color = currentColor;
    }

    void OnBeatDetected() {
        Debug.Log("Color");
        currentColor = Color.red;
    }
}
