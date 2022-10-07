using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCloud : MonoBehaviour
{
    //プレハブ格納
    public GameObject[] Prefabcloud;

    float time = 3;


    void FixedUpdate()
    {
        time -= Time.deltaTime;
        //プレハブを生成する頻度（フレーム数）
        if (time < 0)
        {
            time = Random.Range(8, 4);
            // プレハブの位置設定
            float y = Random.Range(5.0f, 8.0f);
            float z = Random.Range(10.0f, 10.0f);
            Vector3 pos = new Vector3(x: 14.0f, y, z);

            // プレハブ生成
            Instantiate(Prefabcloud[Random.Range(0, 2)], pos, Quaternion.identity);
        }    
    }
}
