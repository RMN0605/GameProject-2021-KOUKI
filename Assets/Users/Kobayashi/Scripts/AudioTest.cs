using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    [SerializeField]AudioManager audioManager;  //オーディオマネージャースクリプトの参照
    // Start is called before the first frame update
    void Start()
    {
        audioManager = AudioManager.instance;   //音流すために必要
    }

    bool isCallOnce = false;


    // Update is called once per frame
    void Update()
    {

        if (!isCallOnce)
        {
            isCallOnce = true;
            audioManager.PlaySound("Test");
        }

    }
}
