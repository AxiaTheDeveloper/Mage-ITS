using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance{get; private set;}
    
    [SerializeField]private Slider SFXSlider;
    [SerializeField]private AudioSource AudioA;
    private const string PLAYER_PREF_SFX_VOLUME = "SFX_Volume";
    private float volume;
    private void Awake() 
    {
        Instance = this;
        if(!PlayerPrefs.HasKey(PLAYER_PREF_SFX_VOLUME))PlayerPrefs.SetFloat(PLAYER_PREF_SFX_VOLUME, 0.3f);
        volume = PlayerPrefs.GetFloat(PLAYER_PREF_SFX_VOLUME);
        UpdateSFX_Volume();
    }
    void Start()
    {
        SFXSlider.value = volume;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateSFX_Volume(){
        volume = SFXSlider.value;
        if(AudioA)AudioA.volume = volume;



        PlayerPrefs.SetFloat(PLAYER_PREF_SFX_VOLUME, volume);
    }
}
