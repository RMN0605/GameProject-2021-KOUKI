using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wind : MonoBehaviour
{
    //---public-------------------------------------------------------    

    [SerializeField] AudioManager audioManager;  //オーディオマネージャースクリプトの参照

    //[HideInInspector]
    public float WP_x = 0;
    //[HideInInspector]
    public float WP_y = 0;
    public int second_power = 1500;
    public int third_power = 4000;
    public float windvaluehold = 0;
    //---private------------------------------------------------------
    private int randomdice;
    private int dice;
    private int m_dice;
    private int tm_dice;
    private float m = 0;
    private float point = 0;
    private bool chenge_delay = true;
    private bool iventflag = false;
    private Vector3 force;
    private float delayytime = 0;
    ///////////////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////////////
    void Start()
    {
        audioManager = AudioManager.instance;   //音流すために必要

        randomdice = 0;
        transform.GetChild(8).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreManager.Score != 0)   //スコアが0以外の場合
        {
            if (m == ScoreManager.Score % 300)　//300m毎の判定
            {
                iventflag = true;
            }
        }
        if (iventflag == true && chenge_delay == true)
        {
            if (point < ScoreManager.Score)
            {
                randomwind();
            }
            point = ScoreManager.Score;
        }

        if (chenge_delay == false)
        {
            delayytime += Time.deltaTime;

            if(delayytime  < 15)
            {
                delayytime = 0;

                chenge_delay = true;
            }
        }
    }

    /// <summary>
    /// 無風
    /// </summary>
    public void windless()
    {
        WP_x = 0;
        WP_y = 0;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(8).gameObject.SetActive(true);
    }
    /// <summary>
    /// 右向き
    /// </summary>
    public void rightwind()
    {
        audioManager.PlaySound("wind");

        WP_x = windvaluehold;  // 力を設定
        WP_y = 0.0f;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(0).gameObject.SetActive(true);
    }
    /// <summary>
    /// 左向き
    /// </summary> 
    public void leftwind()
    {
        audioManager.PlaySound("wind");

        WP_x = -windvaluehold;    // 力を設定

        WP_y = 0.0f;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
    }
    /// <summary>
    /// 上向き
    /// </summary>
    public void upwind()
    {
        audioManager.PlaySound("wind");

        WP_y = windvaluehold;  // 力を設定
        WP_x = 0.0f;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(2).gameObject.SetActive(true);
    }
    /// <summary>
    /// 下向き
    /// </summary>
    public void downwind()
    {
        audioManager.PlaySound("wind");

        WP_y = -windvaluehold; // 力を設定
        WP_x = 0.0f;
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(3).gameObject.SetActive(true);
    }
    /// <summary>
    /// 斜め右上
    /// </summary>
    public void upperright()
    {
        audioManager.PlaySound("wind");

        WP_x = windvaluehold;
        WP_y = windvaluehold; // 力を設定
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(4).gameObject.SetActive(true);
    }
    /// <summary>
    /// 斜め右下
    /// </summary>
    public void lowerright()
    {
        audioManager.PlaySound("wind");

        WP_x = windvaluehold;
        WP_y = -windvaluehold;    // 力を設定
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(5).gameObject.SetActive(true);
    }
    /// <summary>
    /// 斜め左上
    /// </summary>
    public void upperleft()
    {
        audioManager.PlaySound("wind");

        WP_x = -windvaluehold;
        WP_y = windvaluehold; // 力を設定
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(6).gameObject.SetActive(true);
    }
    /// <summary>
    /// 斜め左下
    /// </summary>
    public void lowerleft()
    {
        audioManager.PlaySound("wind");

        WP_x = -windvaluehold;
        WP_y = -windvaluehold;  // 力を設定
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        transform.GetChild(7).gameObject.SetActive(true);
    }
    /// <summary>
    /// 風抽選
    /// </summary>
    public void randomwind()
    {
        m_dice = (Random.Range(1, 10));
        if (ScoreManager.Score >= second_power)　//風の強さの数値をいじる場所　3000M
        {
            if (m_dice >= 3)
            {
                windvaluehold = 0.1f;
            }
            else if (m_dice <= 4 && m_dice >= 7)
            {
                windvaluehold = 0.2f;
            }
            else
            {
                windvaluehold = 0.3f;
            }
        }
        else if (ScoreManager.Score >= second_power)　//風の強さの数値をいじる場所　1500M
        {
            if (m_dice >= 5)
            {
                windvaluehold = 0.1f;
            }
            else
            {
                windvaluehold = 0.2f;
            }
        }
        else
        {
            windvaluehold = 0.1f;
        }
        dice = (Random.Range(1, 10));
        //Debug.Log(dice);
        if (dice == 1)
        {
            dice1();
            iventflag = false;
        }
        chenge_delay = false;
    }
    /// <summary>
    /// 物置
    /// </summary>
    public void dice1()
    {
        randomdice = (Random.Range(0, 8));
        //Debug.Log("風の抽選" + randomdice);
        switch (randomdice)
        {
            case 0:
                windless();
                break;
            case 1:
                rightwind();
                break;
            case 2:
                leftwind();
                break;
            case 3:
                upwind();
                break;
            case 4:
                downwind();
                break;
            case 5:
                upperright();
                break;
            case 6:
                lowerright();
                break;
            case 7:
                upperleft();
                break;
            case 8:
                lowerleft();
                break;
        }
    }
}
