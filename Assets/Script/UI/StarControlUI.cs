using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StarControlUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI[] starTotalMovesText;
    [SerializeField]private GameObject[] stars;
    
    public void ChangeTotalMoves(int[] moves)
    {
        for(int i=0;i<starTotalMovesText.Length;i++)
        {
            starTotalMovesText[i].text = moves[i].ToString();
        }
    }
    public void ChangeStarsVisual(int score)
    {
        for(int i = 0;i<score;i++)
        {
            stars[i].SetActive(true);
        }
    }

}
