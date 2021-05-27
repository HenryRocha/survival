using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Sound[] sounds;
    [SerializeField] private Sound[] scaredShoutsSounds;

    private float scaredShoutLastPlayed = -5.0f;
    private float scaredShoutTimeDiff = 5.0f;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
        }
        foreach (Sound sound in scaredShoutsSounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
        }
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        Play("bgm");
    }

    private void FixedUpdate()
    {
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound != null)
        {
            sound.source.Play();
        }
        else
        {
            Debug.LogWarning($"Could not find audio with name '{name}'");
        }
    }

    public void PlayScaredShout()
    {

        if (Time.time - scaredShoutLastPlayed > scaredShoutTimeDiff)
        {
            Debug.LogWarning($"Time of shout: '{Time.time}'");
            int shout = UnityEngine.Random.Range(0, scaredShoutsSounds.Length);
            Sound sound = scaredShoutsSounds[shout];
            Debug.LogWarning($"Shout index: '{shout}'");
            Debug.LogWarning($"Shout name: '{sound.name}'");

            sound.source.Play();
            scaredShoutLastPlayed = Time.time;

        }
    }

    public void ResetAndStop(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound != null)
        {
            sound.source.Stop();
            sound.source.time = 0f;
        }

        foreach (Sound s in sounds)
        {
            Debug.Log($"Sound name: {s.name}");
        }
    }

    public void SetVolume(string name, float volume)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound != null)
        {
            sound.source.volume = volume;
        }
    }
}
