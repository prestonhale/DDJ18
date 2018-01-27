using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{
    public float timeSinceLastBeat = 0f;
    public float timeOfLastBeat = 0f;
    public AudioClip musicClip;
    public SimpleBeatDetection beatDetector;

    Camera _camera;
    AudioSource _audioSource;

    public static Music Instance;

    void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _camera = Camera.main;
        if (_camera != null)
        {
            _audioSource = _camera.GetComponent<AudioSource>();
            beatDetector = _camera.GetComponent<SimpleBeatDetection>();
        }
        if (_audioSource != null)
        {
            _audioSource.clip = musicClip;
            _audioSource.playOnAwake = false;
        }
        if (beatDetector != null)
        {
            _audioSource.clip = musicClip;
            beatDetector.audioSource = _audioSource;
            beatDetector.bufferSize = 1024;
            beatDetector.OnBeat += OnBeatDetected;
        }
        timeSinceLastBeat = 0f;
        timeOfLastBeat = 0f;
    }

    void Update() {
        timeSinceLastBeat = Time.time - timeOfLastBeat;
    }

    void OnBeatDetected()
    {
        timeOfLastBeat = Time.time;
    }

    public void Play()
    {
        _audioSource.Play();
    }
}
