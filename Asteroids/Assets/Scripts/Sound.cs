using UnityEngine.Audio;
using UnityEngine;

[System.Serializable] // To see within the editor
public class Sound {

    public string name;

    public AudioClip clip;

    [Range(0f, 1f)] // Gives slider within the editor
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector] // Hides the variable form editor
    public AudioSource source;

}
