using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;

namespace SCSIA
{
    public class AudioManager : MonoBehaviour
    {
        //############################################################################################
        // FIELDS
        //############################################################################################
        private AudioManagerConfig _audioManagerConfig;
        private AudioSource _musicSource;
        private AudioSource _sfxSource;

        //############################################################################################
        // PUBLIC METHODS
        //############################################################################################
        public void Init(AudioManagerConfig audioManagerConfig)
        {
            _audioManagerConfig = audioManagerConfig;
            _musicSource = CreateAudioSource("MusicSource", _audioManagerConfig.MusicGroup);
            _sfxSource = CreateAudioSource("SFXSource", _audioManagerConfig.SfxGroup);
        }

        public void PlayMusic(AudioClip clip, float volume = 1.0f, bool loop = true)
        {
            _musicSource.clip = clip;
            _musicSource.loop = loop;
            _musicSource.volume = volume;
            _musicSource.Play();
        }

        public void StopMusic()
        {
            _musicSource.Stop();
        }

        public void PauseMusic()
        {
            _musicSource.Pause();
        }

        public void UnPauseMusic()
        {
            _musicSource.UnPause();
        }

        public void PlaySFX(AudioClip clip, float volume = 1.0f)
        {
            _sfxSource.volume = volume;
            _sfxSource.PlayOneShot(clip);
        }

        //############################################################################################
        // PRIVATE METHODS
        //############################################################################################
        private AudioSource CreateAudioSource(string name, AudioMixerGroup group)
        {
            AudioSource source = new GameObject(name).AddComponent<AudioSource>();
            source.transform.SetParent(transform);
            source.outputAudioMixerGroup = group;
            source.volume = 1.0f;
            source.loop = false;
            source.playOnAwake = false;
            return source;
        }
    }
}
