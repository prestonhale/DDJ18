using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BeatObserver : MonoBehaviour
{
    public AudioClip clip;
    public Transform cube;
    public List<BeatListener> listeners = new List<BeatListener>();
    public SimpleBeatDetection beatDetector;
    void Start()
    {
        beatDetector.OnBeat += OnBeatDetected;
    }

    void Update()
    {

    }

    public void register(BeatListener listener){
        listeners.Add(listener);
        Debug.Log("Registered");
    }

    void OnBeatDetected()
    {
        StartCoroutine(FlashCube());
        for (int i = 0; i < listeners.Count; i++) {
            listeners[i].OnBeat();
        }
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
