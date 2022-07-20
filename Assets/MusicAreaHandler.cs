using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAreaHandler : MonoBehaviour
{
    AudioSource source;
    public AudioClip[] musicClips;

    void Start()
    {
        source = GetComponent<AudioSource>();
        StartCoroutine(playMusic());   
    }

    IEnumerator playMusic()
    {
        // Plays Intro
        /* int i = 0;
        bool maxLengthReached = false;
        while (true)
        {
            //Plays current clip
            source.clip = musicClips[i];
            source.Play();

            // Checks to see if the final clip is already selected
            if (musicClips[i + 1] || !maxLengthReached) {
                // Waits for the current clip to finish playing, and selects the next clip for playing
                yield return new WaitForSeconds(source.clip.length);
                i++;
            } else {
                // Stops the clip cycler
                maxLengthReached = true;
            }
        } */

        source.playOnAwake = false;
        source.loop = false;
        for (int i = 0; i < musicClips.Length; i++)
        {
            //Plays current clip
            source.clip = musicClips[i];
            source.Play();
            if (i == musicClips.Length - 1) break;
            while (source.isPlaying) yield return null;
        }
        source.loop = true;
    }
}
