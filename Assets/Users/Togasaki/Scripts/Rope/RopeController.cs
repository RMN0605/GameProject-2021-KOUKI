using UnityEngine;
using System;
using UnityEngine.EventSystems;
using Unity.Mathematics;

public class RopeController : MonoBehaviour
{
    [SerializeField, Header("RopeHingeオブジェクト")]
    private GameObject hingeObj;

    [SerializeField, Header("綱のHingeの数"), Range(10, 1000)]
    private int hingesLength = 100;

    [SerializeField, Header("ロープのX座標の端")]
    private float ropeLength = 12;

    [SerializeField, Header("Hingesオブジェクト(Hingeの親)")]
    private Transform hingesParent;

    /// <summary>
    ///	何個の座標の幅を引っ張るか(半径)
    /// </summary>
    public static int pullWidth = 15;

    /// <summary>
    /// メインカメラをStartで格納
    /// </summary>
    private Camera mainCamera;

    /// <summary>
    /// 綱のHingeオブジェクトの配列
    /// </summary>
    public static Transform[] hinges;

    /// <summary>
    /// hinges内のSpringを配列として格納
    /// </summary>
    public static Spring[] hingeSpringRef;

    /// <summary>
    /// LineRendererをStartで格納
    /// </summary>
    private LineRenderer lineRendererRef;

    /// <summary>
    /// 引っ張っているロープの頂点
    /// </summary>
    public static int apex;

    /// <summary>
    /// Rayの長さ
    /// </summary>
    float maxRayDistance = 5;

    private void Start()
    {
        mainCamera = Camera.main;

        HingesGenerate();

    }

    private void Update()
    {
        AdjustLine();
        //FingerFunc();
    }

    /// <summary>
    /// HingeをhingesLengthの数だけ生成し、生成したHingeを配列hingesに格納する関数
    /// その際Hingeオブジェクトは非表示にする
    /// </summary>
    void HingesGenerate()
    {
        lineRendererRef = GetComponent<LineRenderer>();
        lineRendererRef.positionCount = hingesLength;
        Array.Resize(ref hinges, hingesLength);
        Array.Resize(ref hingeSpringRef, hingesLength);

        float rL = -ropeLength;
        float rLRatio = (ropeLength * 2) / hingesLength;

        //hingeを生成し、hingesに格納
        for (int i = 0; i < hingesLength; i++)
        {
            GameObject child = Instantiate(hingeObj, new Vector3(rL, 0, 0), Quaternion.identity);
            child.transform.parent = hingesParent;
            hinges[i] = child.transform;
            hingeSpringRef[i] = hinges[i].GetComponent<Spring>();
            hingeSpringRef[i].hingeNum = i;
            //SpriteRenderer非表示
            child.GetComponent<SpriteRenderer>().enabled = false;
            rL += rLRatio;
        }
    }

    /// <summary>
    /// LineRendererをHingesに合わせる関数
    /// </summary>
    void AdjustLine()
    {
        int i = 0;

        foreach (Transform v in hinges)
        {
            lineRendererRef.SetPosition(i, v.position);
            i++;
        }

    }

    /// <summary>
    /// 入力検知
    /// </summary>
    void FingerFunc()
    {
        var touchCount = Input.touchCount;

        if (touchCount <= 0)
            return;

        for (var i = 0; i < touchCount; i++)
        {
            if (i > 1)
            {
                break;
            }

            var touchPos = mainCamera.ScreenToWorldPoint(Input.GetTouch(i).position);
            Vector3 screenPos = new Vector3(touchPos.x, touchPos.y, -1);
            Ray ray = mainCamera.ScreenPointToRay(screenPos);

            RaycastHit2D hit = Physics2D.Raycast(screenPos,new Vector3(0,0,1),maxRayDistance);

            //ここで触れたSpringのSelfPressedをtrueに
            if (hit.collider && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Hinge")
            {
                Spring spr = hit.collider.gameObject.GetComponent<Spring>();
                spr.selfPressed = true;
                RopeController.apex = spr.hingeNum;

                for (int j = 0; j < pullWidth; j++)
                {
                    if (spr.hingeNum - j < 0)
                    {
                        break;
                    }

                    RopeController.hingeSpringRef[spr.hingeNum - j].subPressed = true;

                }

                for (int j = 0; j < pullWidth; j++)
                {
                    if (spr.hingeNum + j > RopeController.hingeSpringRef.Length - 1)
                    {
                        break;
                    }

                    RopeController.hingeSpringRef[spr.hingeNum + j].subPressed = true;

                }

            }


        }

    }


}