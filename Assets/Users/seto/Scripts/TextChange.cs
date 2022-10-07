
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TextChange : MonoBehaviour
{
    public string[] texts; //Unity上で入力するstringの配列
    int textNumber; //何番目のtexts[]を表示させるか
    string displayText; //表示させるstring
    int textCharNumber; //何文字目をdisplayTextに追加するか
    int displayTextSpeed; //全体のフレームレートを落とす変数
    bool click; //クリック判定
    bool textStop; //テキスト表示を始めるか

    [SerializeField, TextArea(1, 3)]
    string[] ts;

    [SerializeField] GameObject Image1;
    [SerializeField] GameObject Image2;
    [SerializeField] GameObject Image3;
    [SerializeField] GameObject TTextObj;
    [SerializeField] Text TText;
    [SerializeField] Image countDownImage;
    [SerializeField] Text countDownText;

    //void Update()
    //{
    //    TextCall();
    //}

    //void TextCall()
    //{
    //    if (textStop == false) //テキストを表示させるif文
    //    {
    //        displayTextSpeed++;
    //        if (displayTextSpeed % 3 == 0) //3回に一回プログラムを実行するif文
    //        {
    //            if (textCharNumber != texts[textNumber].Length) //もしtext[textNumber]の文字列の文字が最後の文字じゃなければ
    //            {
    //                displayText = displayText + texts[textNumber][textCharNumber]; //displayTextに文字を追加していく
    //                textCharNumber = textCharNumber + 1; //次の文字にする
    //            }
    //            else //もしtext[textNumber]の文字列の文字が最後の文字だったら
    //            {
    //                if (textNumber != texts.Length - 1) //もしtexts[]が最後のセリフじゃないときは
    //                {
    //                    if (click == true) //クリックされた判定
    //                    {
    //                        displayText = ""; //表示させる文字列を消す
    //                        textCharNumber = 0; //文字の番号を最初にする
    //                        textNumber = textNumber + 1; //次のセリフにする
    //                    }
    //                }
    //                else //もしtexts[]が最後のセリフになったら
    //                {
    //                    if (click == true) //クリックされた判定
    //                    {
    //                        displayText = ""; //表示させる文字列も消す
    //                        textCharNumber = 0; //文字の番号を最初にする
    //                        textStop = true; //セリフ表示を止める
    //                    }
    //                }
    //            }

    //            this.GetComponent<Text>().text = displayText; //画面上にdisplayTextを表示
    //            click = false; //クリックされた判定を解除
    //        }

    //        if (Input.GetKeyDown(KeyCode.A)) //マウスをクリックしたら
    //        {
    //            click = true; //クリックされた判定にする
    //        }
    //    }
    //}



    //試遊会ではここから下を使います

    [SerializeField]
    float maxCount = 7;

    float count;

    [SerializeField]
    GameObject dummy;

    [SerializeField]
    Transform point;

    private void Start()
    {
        count = maxCount;
        TText.text = ts[0];
        Invoke("DelayMethod", 0.1f);

    }

    public void NextText()
    {
        if (textNumber < ts.Length-1)
        {
            if(textNumber == 0)
            {
                Image1.SetActive(false);
                Image2.SetActive(true);
                TText.text = ts[textNumber + 1];
                textNumber++;

            }
            else if(textNumber == 1 || textNumber == 2)
            {
                Time.timeScale = 1;
                Image2.SetActive(false);
                TTextObj.SetActive(false);
                countDownImage.gameObject.SetActive(true);
                if(textNumber == 2)
                {
                    count = 8;
                    Image3.SetActive(false);
                }
                StartCoroutine("ExplainTimeMethod");
            }
            else if(textNumber == 3)
            {
                SceneManager.LoadScene("MenuScene");
            }

        }
    }

    IEnumerator ExplainTimeMethod()
    {
        int seconds;

        countDownText.text = count.ToString();
        while(count>1)
        {
            count -= Time.deltaTime;
            if(countDownImage.fillAmount >= 1)
            {
                countDownImage.fillAmount = 0;
            }
            countDownImage.fillAmount += Time.deltaTime;
            seconds = (int)count;
            countDownText.text = seconds.ToString();
            yield return null;

        }

        textNumber++;
        if(textNumber == 2)
        {
            Image3.SetActive(true);
            Instantiate(dummy, point.position, Quaternion.identity);
        }
        Time.timeScale = 0;
        TText.text = ts[textNumber + 1];
        TTextObj.SetActive(true);
        countDownImage.gameObject.SetActive(false);
        count = maxCount;
    }

    void DelayMethod()
    {
        Time.timeScale = 0;
    }



}
//GetMouseButtonDown(0)