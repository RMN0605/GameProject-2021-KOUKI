using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWall : MonoBehaviour
{
    [SerializeField, Header("メインカメラ")]
    private Camera _mainCamera;

    /// <summary>
    /// メインカメラの左下端
    /// </summary>
    private Vector3 topLeft;

    /// <summary>
    /// 自分がどこのブロックなのか
    /// </summary>
    public bool _left = false;
    public bool _right = false;
    public bool _top = false;

    void Start()
    {
        topLeft = _mainCamera.ScreenToWorldPoint(Vector3.zero);

        //左の場合
        if(_left)
        {
            if (topLeft.x < -15)
            {
                gameObject.transform.position = new Vector3(-12.5f, 0, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(topLeft.x - 0.5f, 0, 0);
            }
        }
        //右の場合
        else if(_right)
        {
            if (topLeft.x < -15)
            {
                gameObject.transform.position = new Vector3(12.5f, 0, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(-topLeft.x + 0.5f, 0, 0);
            }
        }
        //上の場合
        else if(_top)
        {
            gameObject.transform.position = new Vector3(0, -topLeft.y + 0.5f, 0);
        }

    }

}
