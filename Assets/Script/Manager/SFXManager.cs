using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance{get; private set;}
    
    [SerializeField]private Slider SFXSlider;
    [SerializeField]private AudioSource buttonCanBeUsed_SFX, buttonCantBeUsed_SFX, onDrag__SFX, onDragRelease_SFX, rotate__SFX;
    private const string PLAYER_PREF_SFX_VOLUME = "SFX_Volume";
    private float volume;
    private void Awake() 
    {
        Instance = this;
        // Debug.Log(PlayerPrefs.GetFloat(PLAYER_PREF_SFX_VOLUME));
        if(!PlayerPrefs.HasKey(PLAYER_PREF_SFX_VOLUME))PlayerPrefs.SetFloat(PLAYER_PREF_SFX_VOLUME, 0.3f);
        volume = PlayerPrefs.GetFloat(PLAYER_PREF_SFX_VOLUME);
        
        
    }
    void Start()
    {
        SFXSlider.value = volume;
        UpdateSFX_Volume();
    }

    public void UpdateSFX_Volume(){
        volume = SFXSlider.value;
        if(buttonCanBeUsed_SFX)buttonCanBeUsed_SFX.volume = volume;
        if(buttonCantBeUsed_SFX)buttonCantBeUsed_SFX.volume = volume;
        if(onDrag__SFX)onDrag__SFX.volume = volume;
        if(onDragRelease_SFX)onDragRelease_SFX.volume = volume;
        if(rotate__SFX)rotate__SFX.volume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREF_SFX_VOLUME, volume);
        // Debug.Log(PlayerPrefs.GetFloat(PLAYER_PREF_SFX_VOLUME));
    }

    public void PlayOnDrag()
    {
        if(onDrag__SFX)onDrag__SFX.Play();
    }
    public void StopOnDrag()
    {
        if(onDrag__SFX)onDrag__SFX.Stop();
    }
    public bool isOnDragPlay()
    {
        return onDrag__SFX.isPlaying;
    }
    public void PlayOnDragRelease()
    {
        if(onDragRelease_SFX)onDragRelease_SFX.Play();
    }

    public void PlayButtonCanBeUsed()
    {
        if(buttonCanBeUsed_SFX)buttonCanBeUsed_SFX.Play();
    }
    public void PlayButtonCantBeUsed()
    {
        // Debug.Log("Play");
        if(buttonCantBeUsed_SFX)buttonCantBeUsed_SFX.Play();
    }
    public void PlayRotate()
    {
        if(rotate__SFX)rotate__SFX.Play();
    }
    
}
