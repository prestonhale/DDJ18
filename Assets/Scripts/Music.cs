using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Music : MonoBehaviour
{
    public float timeSinceLastBeat = 0f;
    public float timeOfLastBeat = 0f;
    public float beatLength = 0f;
    public AudioClip musicClip;
    public SimpleBeatDetection beatDetector;
    public AudioMixerGroup nullOutputGroup;

    Camera _camera;
    AudioSource _audioSource;
    AudioSource _drumAudioSource;
    AudioSource _sfxAudioSource;

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
            AudioSource[] audioSources = _camera.GetComponents<AudioSource>();
            _audioSource = audioSources[0];
            _drumAudioSource = audioSources[1];
            _sfxAudioSource = audioSources[2];
            beatDetector = _camera.GetComponent<SimpleBeatDetection>();
        }
        if (_audioSource != null)
        {
            _audioSource.playOnAwake = false;
        }
        if (beatDetector != null)
        {
            beatDetector.audioSource = _drumAudioSource;
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
        beatLength = timeSinceLastBeat;
        timeOfLastBeat = Time.time;
    }

    public void Play()
    {
        _drumAudioSource.volume = 1;
        _drumAudioSource.outputAudioMixerGroup = nullOutputGroup;
        _drumAudioSource.loop = true;
        _audioSource.outputAudioMixerGroup = null;
        _audioSource.volume = 1;
        _audioSource.loop = true;
        _drumAudioSource.Play();
        _audioSource.Play();
    }

    public void PlaySFX(AudioClip clip) {
        _sfxAudioSource.clip = clip;
        _sfxAudioSource.volume = 1;
        _sfxAudioSource.loop = false;
        _sfxAudioSource.Play();
    }
}
