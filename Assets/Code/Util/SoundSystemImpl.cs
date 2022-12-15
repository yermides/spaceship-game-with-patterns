using UnityEngine;
using UnityEngine.Audio;

namespace Code.Util
{
    public class SoundSystemImpl : MonoBehaviour, ISoundSystem
    {
        [SerializeField] private AudioMixer masterAudioMixer;
        [SerializeField] private AudioMixer musicAudioMixer;
        [SerializeField] private AudioMixer sfxAudioMixer;
        
        public void PlayOneShot()
        {
            // TODO:
            throw new System.NotImplementedException();
        }
    }
}