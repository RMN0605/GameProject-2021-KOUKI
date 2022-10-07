using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchTest : MonoBehaviour
{

    void Update()
    {
        var touchCount = Input.touchCount;

        for (var i = 0; i < touchCount; i++)
        {
            var touch = Input.GetTouch(i);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // 画面に指が触れた時に行いたい処理をここに書く
                    break;
                case TouchPhase.Moved:
                    // 画面上で指が動いたときに行いたい処理をここに書く
                    break;
                case TouchPhase.Stationary:
                    // 指が画面に触れているが動いてはいない時に行いたい処理をここに書く
                    break;
                case TouchPhase.Ended:
                    // 画面から指が離れた時に行いたい処理をここに書く
                    break;
                case TouchPhase.Canceled:
                    // システムがタッチの追跡をキャンセルした時に行いたい処理をここに書く
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
