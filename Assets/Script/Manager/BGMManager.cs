using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMManager : MonoBehaviour
{
    [SerializeField]private AudioSource BGM;
    private float volume;
    [SerializeField]private Slider bgmSlider;
    private const string PLAYER_PREF_BGM_VOLUME = "BGM_Volume";
    public static BGMManager Instance{get; private set;}
    [SerializeField]private float fadeInDurationMax = 0.5f;
    private float fadeInDuratiom;
    [SerializeField]private bool isMainMenu;
    private void Awake() {
        if(!Instance){
            // Debug.Log(BGM.volume);
            if(!PlayerPrefs.HasKey(PLAYER_PREF_BGM_VOLUME))PlayerPrefs.SetFloat(PLAYER_PREF_BGM_VOLUME, 0.3f);
            volume = PlayerPrefs.GetFloat(PLAYER_PREF_BGM_VOLUME);
            // Debug.Log(volume);
            fadeInDuratiom = 0;
            if(BGM)BGM.volume = 0f;
            
            
            // Debug.Log(BGM.volume);
            StartCoroutine(fadeIn());
            Instance = this;
            if(!isMainMenu){
                DontDestroyOnLoad(gameObject);
                // Debug.Log("halo??");
            }
            
        }
        else{
            Destroy(gameObject);
        }
    }


    private void Update() {
        if(bgmSlider == null){
            bgmSlider = GameObject.FindWithTag("BGMSlider").transform.parent.transform.parent.GetComponent<PauseUI>().GetBGMSlider();
            float saveVolume = volume;
            if(bgmSlider.value == volume)bgmSlider.value = 0;
            bgmSlider.value = saveVolume;
            bgmSlider.onValueChanged.AddListener(UpdateBGM_Volume);
        }
    }

    public void UpdateBGM_Volume(float a){
        // Debug.Log("test");
        volume = bgmSlider.value;
        // Debug.Log(volume);
        if(BGM)BGM.volume = volume;
        
        PlayerPrefs.SetFloat(PLAYER_PREF_BGM_VOLUME, volume);
        
    }
    public void DestroyInstance(){
        Destroy(gameObject);
    }
    private IEnumerator fadeIn()
    {
        if(bgmSlider)
        {
            float saveVolume = volume;
            if(bgmSlider.value == volume)bgmSlider.value = 0;
            bgmSlider.value = saveVolume;
        }
        
        if(BGM)BGM.Play();
        while(fadeInDuratiom < fadeInDurationMax )
        {
            fadeInDuratiom += 0.01f;
            // Debug.Log(fadeInDuratiom + " " + Mathf.Lerp(0f, volume, fadeInDuratiom/fadeInDurationMax));
            if(BGM)BGM.volume = Mathf.Lerp(0f, volume, fadeInDuratiom/fadeInDurationMax); 
            // Debug.Log(BGM.volume);      
            yield return new WaitForSeconds(0.1f);
        }
        if(BGM)BGM.volume = volume;
    }
    // public void PlayBGM()
    // {
    //     BGM.Play();
    //     StartCoroutine(fadeIn());
    // }
    // public void StopBGM()
    // {
    //     BGM.Stop();
    //     volume = BGM.volume;
    //     BGM.volume = 0f;
    //     wasStop = true;
    // }
    // public bool isPlayedBGM()
    // {
    //     return BGM.isPlaying;
    // }
}
