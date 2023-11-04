using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Duarto.Utilities;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Duarto.PPTP.Manager{
    public class AudioManager : Singleton<AudioManager> {

        [Header("Manage audio")]
        public AudioMixer mixer;
        public Slider masterVolumeSlider;
        public Slider musicVolumeSlider;
        public Slider SFXVolumeSlider;

        [Header("AUDIO SOURCE")]
        [Header("Musica")]
        public AudioSource as_musicIngame;
        public AudioSource as_musicMenu;
        [Header("SFX")]
        public AudioSource as_hit;
        public AudioSource as_empate;
        public AudioSource as_takeDamage;
        [Header("SFX (UI)")]
        public AudioSource as_clic;
        public AudioSource as_buttonClic;
        public AudioSource as_quit;
        public AudioSource as_play;


        private void Start() {
            if(PlayerPrefs.HasKey("masterVolume")) {
            } else { PlayerPrefs.SetFloat("masterVolume",1); }

            if(PlayerPrefs.HasKey("musicVolume")) {
            } else { PlayerPrefs.SetFloat("musicVolume",1); }

            if(PlayerPrefs.HasKey("sfxVolume")) {
            } else { PlayerPrefs.SetFloat("sfxVolume",1); }

            masterVolumeSlider.value = PlayerPrefs.GetFloat("masterVolume");
            ChangeMasterVolumenBySlider();
            musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
            ChangeMusicVolumenBySlider();
            SFXVolumeSlider.value = PlayerPrefs.GetFloat("sfxVolume");
            ChangeSFXVolumenBySlider(); 
        }

//**********PLAY/STOP AUDIOS**********//

        public void StartIngameMusic() {
            as_musicIngame.Play();
        }
        public void StopIngameMusic() {
            as_musicIngame.Stop();
        }
        public void StartMenuMusic() {
            as_musicMenu.Play();
        }
        public void StopMenuMusic() {
            as_musicMenu.Stop();
        }

        public void PlayHit() {
            as_hit.Play();
        }
        public void PlayDamage() {
            as_takeDamage.Play();
        }
        public void PlayEmpate() {
            as_empate.Play();
        }

        public void PlayClic() {
            as_clic.Play();
        }
        public void PlayButtonClic() {
            as_buttonClic.Play();
        } 
        public void PlayQuit() {
            as_quit.Play();
        }
        public void PlayPlay() {
            as_play.Play();
        }

//**********AUDIO SETTINGS**********//

        public void ChangeMasterVolumenBySlider() { // Modify the sound volume 
            mixer.SetFloat("MasterVolume",Mathf.Log10(masterVolumeSlider.value)*20);
            PlayerPrefs.SetFloat("masterVolume",masterVolumeSlider.value);
        }

        public void ChangeMusicVolumenBySlider() { // Modify the sound volume 
            mixer.SetFloat("MusicVolume",Mathf.Log10(musicVolumeSlider.value)*20);
            PlayerPrefs.SetFloat("musicVolume",musicVolumeSlider.value);
        }

        public void ChangeSFXVolumenBySlider() { // Modify the sound volume 
            mixer.SetFloat("SFXVolume",Mathf.Log10(SFXVolumeSlider.value)*20);
            PlayerPrefs.SetFloat("sfxVolume",SFXVolumeSlider.value);
        }

    }
}