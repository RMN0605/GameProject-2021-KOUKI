using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAttackAudio : MonoBehaviour
{
    private AudioManager audioManagerS;  //オーディオマネージャースクリプトの参照
    // Start is called before the first frame update
    void Start()
    {
        audioManagerS = AudioManager.instance;   //音流すために必要
        
        audioManagerS.PlaySound("ScoreAttackBGM");
    }
}
