using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyHighScore : MonoBehaviour
{
    [SerializeField, Header("自分のハイスコアテキスト")]
    private Text _myHighScoreText;
    
    private void Start()
    {
        if(PlayerPrefs.HasKey("myScore"))
        {
            _myHighScoreText.text = PlayerPrefs.GetFloat("myScore").ToString() + "M";
        }
        else
        {
            _myHighScoreText.text = "0000M";
        }
    }
}
