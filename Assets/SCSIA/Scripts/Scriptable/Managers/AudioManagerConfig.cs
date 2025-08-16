using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "AudioManagerConfig", menuName = "Scriptable Objects/Manager/AudioManagerConfig")]
public class AudioManagerConfig : ScriptableObject
{
    //############################################################################################
    // FIELDS
    //############################################################################################
    [Header("Mixer")]
    [SerializeField] private AudioMixerGroup _musicGroup;
    [SerializeField] private AudioMixerGroup _sfxGroup;

    //############################################################################################
    // PROPERTIES
    //############################################################################################
    public AudioMixerGroup MusicGroup => _musicGroup;
    public AudioMixerGroup SfxGroup => _sfxGroup;
}
