using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
using static Unity.Burst.Intrinsics.X86.Avx;

public class StarControlUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI[] starTotalMovesText;
    [SerializeField]private GameObject[] stars;
    [SerializeField]private ParticleSystem[] starParticles;
    [SerializeField]private CinemachineVirtualCamera cm;
    [SerializeField]private float amplitude, frequency, duration;

    private void Start()
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) StartCoroutine(Shake());
    }

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
            SFXManager.Instance.PlayNoStars();
        }
        StartCoroutine(Shake());
    }
    public void ShakeScreen(float amp)
    {
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amp;
    }

    public IEnumerator Shake()
    {
        LeanTween.value(gameObject, ShakeScreen, amplitude, 0f, duration);
        yield return null;
    }

}
