using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [HideInInspector] 
    public static float Score;       //スコアの値保存用

    private void Awake()
    {
        Score = 0;
    }

}
