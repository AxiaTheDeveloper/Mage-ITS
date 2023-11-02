using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;

public class StarControlUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI[] starTotalMovesText;
    [SerializeField]private GameObject[] stars;
    [SerializeField]private ParticleSystem[] starParticles;
    [SerializeField]private CinemachineVirtualCamera cm;
    [SerializeField]private float amplitude, frequency, duration;

    
    public void ChangeTotalMoves(int[] moves)
    {
        for(int i=0;i<starTotalMovesText.Length;i++)
        {
            starTotalMovesText[i].text = moves[i].ToString();
        }
    }
    public void DestroyStarVisual(int score)
    {
        if(score == 2)
        {
            stars[0].SetActive(false);
            starParticles[0].Play();
        }
        else if(score == 1)
        {
            stars[1].SetActive(false);
            starParticles[1].Play();
        }
        else if(score == 0)
        {
            stars[2].SetActive(false);
            starParticles[2].Play();
        }
        StartCoroutine(Shake());
    }
    public void ShakeScreen(float amp, float fre)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amp;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = fre;
    }
    public IEnumerator Shake()
    {
        ShakeScreen(amplitude, frequency);
        yield return new WaitForSeconds(duration);
        ShakeScreen(0, 0);
    }

}
