using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
namespace CrossyRoad.Audio
{
    public class AudioController : MonoBehaviour
    {
        [SerializeField] private AudioMixerGroup mixer;
        [SerializeField] private Toggle toggleMusic;        //Volume toggle(on/off)
        private void Start()
        {
            toggleMusic.isOn = PlayerPrefs.GetInt("MasterVolume") == 1;
        }
        public void ToggleMusic(bool enabled)
        {
            if (enabled)
            {
                mixer.audioMixer.SetFloat("MasterVolume", 0f);
            }
            else
            {
                mixer.audioMixer.SetFloat("MasterVolume", -80f);
            }
            PlayerPrefs.SetInt("MasterVolume", enabled ? 1 : 0);
        }
    }
}