using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundeffectsManager : MonoBehaviour {
    static SoundeffectsManager main;
    static AudioSource mainsource;
    static AudioSource[] sources;
    public AudioClip[] clips;

    static Dictionary<string, AudioClip> clipdics = new Dictionary<string, AudioClip>();


    // Use this for initialization
    void Awake () {
        main = this;
        sources = GetComponents<AudioSource>();
        foreach (AudioSource sur in sources) {
            sur.volume = 0.5f;
        }
        mainsource = sources[0];
        mainsource.playOnAwake = false;
        foreach (AudioClip clip in clips) {
            if (clipdics.ContainsKey(clip.name)) continue;
            clipdics.Add(clip.name, clip);
        }
    }

    static public bool playingSound()
    {
        foreach (AudioSource source in sources)
        {
            if (source.isPlaying) return true;
        }
        return false;
    }

    // Update is called once per frame
    static public void PlayEffect (string key) {
        if (sources == null) return;
        foreach (AudioSource source in sources)
        {
            if (source == null) return;
            if (source.isPlaying) continue;
            if (!clipdics.ContainsKey(key)) continue; 
            source.clip = clipdics[key];
            source.Play();
            return;
        }
    }
}
