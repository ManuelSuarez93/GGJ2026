using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using FMODUnityResonance;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
               Debug.Log("No game manager instance");
            }

            return instance;
        }
    }

    [Header("Volume")]
    [Range(0, 10)]
    [SerializeField] private float masterVolume = 1;
    [Range(0, 10)]
    [SerializeField] private float musicVolume = 1;
    [Range(0, 10)]
    [SerializeField] private float ambienceVolume = 1;
    [Range(0, 10)]
    [SerializeField] private float SFXVolume = 1;

    private Bus masterBus;
    private Bus musicBus;
    private Bus ambienceBus;
    private Bus sfxBus;

    private List<EventInstance> eventInstances;
    [SerializeField] private StudioEventEmitter musicEmitter;
    [SerializeField] private StudioEventEmitter footstepEmitter;

    private void Awake()
    { 
        instance = this;  
        masterBus = RuntimeManager.GetBus("bus:/");
        masterBus.setVolume(masterVolume);
        StartMusic();
    }

    void Update()
    {
        masterBus.setVolume(masterVolume);
    }
    private void StartMusic()
    {
        musicEmitter.Play();
    }

    public void PlayFootstep()
    {
        footstepEmitter.Play();
    }
}
