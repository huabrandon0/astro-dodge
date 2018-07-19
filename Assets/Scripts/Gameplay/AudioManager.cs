using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    protected AudioManager() { }

    [System.Serializable]
    public struct AudioSourceLabel
    {
        public AudioSource audioSource;
        public string label;
    }

    public AudioSourceLabel[] _audioSources;
    Dictionary<string, AudioSource> _audioSourceDict = new Dictionary<string, AudioSource>();
    
    void Awake()
    {
        // Populate the dictionary with the audio sources.
        foreach (AudioSourceLabel audioSource in _audioSources)
            _audioSourceDict.Add(audioSource.label.ToUpper(), audioSource.audioSource);
    }

    public AudioSource GetAudioSource(string key)
    {
        AudioSource ret;
        if (_audioSourceDict.TryGetValue(key.ToUpper(), out ret))
            return ret;
        else
            return null;
    }

    public bool TryPlayAudioSource(string key)
    {
        AudioSource audioSource;
        if (_audioSourceDict.TryGetValue(key.ToUpper(), out audioSource))
        {
            audioSource.Play();
            return true;
        }
        else
            return false;
    }
}
