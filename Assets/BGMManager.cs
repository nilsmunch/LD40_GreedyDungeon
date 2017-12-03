using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour {

    public AudioClip[] musics;
    AudioSource player;


	// Use this for initialization
	void Start () {
        player = GetComponent<AudioSource>();
        putOnSomething();
    }

    void putOnSomething() {
        player.clip = musics[Random.Range(0,musics.Length)];
        player.Play();
    }
	
    // Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.N)) {
            putOnSomething();

        }
        if (!player.isPlaying) {
            putOnSomething();
        }
		
	}
}
