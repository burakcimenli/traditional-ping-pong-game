using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public enum AudioFX {
        Bounce,
        Score
    }

    private static AudioSource audioSource;

    public static void Init(AudioSource source) {
        audioSource = source;
    }

    public static void Play(AudioFX fx) {

    }


}
