using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeatObserver : MonoBehaviour
{
    public AudioClip clip;
    public Transform cube;
    public SimpleBeatDetection beatDetector;
    void Start()
    {
        beatDetector.OnBeat += OnBeatDetected;
    }

    void Update()
    {

    }

    void OnBeatDetected()
    {
        StartCoroutine(FlashCube());
    }

    void OnSpectrum()
    {

    }

    IEnumerator FlashCube() {
        Renderer cubeRenderer = cube.GetComponent<Renderer>();
        cubeRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        cubeRenderer.material.color = Color.white;
        yield return null;
    }
}
