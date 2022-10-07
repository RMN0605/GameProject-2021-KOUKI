using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollManager : MonoBehaviour
{
    [SerializeField] private float ScrollSpeed; //スクロールする速さ
    [SerializeField] Text text;                 //テキスト参照
    private float time;
    private int vecY;                           //雲を生成する位置のY座標
    public GameObject[] Cloud;                    //雲オブジェクト参照
    public MoveCloud moveCloud;                 //ムーブスクリプト参照
    public GameManager gameManager;             //ゲームマネージャースクリプト参照
    // Start is called before the first frame update
    public void Update()
    {
        time -= Time.deltaTime;
        if (time < 0.0f)
        {
            vecY = Random.Range(5, 8);
            int rc = Random.Range(0, 2);
            Instantiate(Cloud[rc], new Vector3(14, vecY, 10), Quaternion.identity);      //雲生成
            time = Random.Range(4f, 8f);
        }
        text.text = ScoreManager.Score.ToString() + "M";    //メートル表示
        Scroll();
    }
    /// <summary>
    /// 敵生成
    /// </summary>
    public void Scroll()
    {
        transform.Translate(ScrollSpeed * Time.deltaTime, 0, 0);        //背景を動かす

        if (transform.position.x < -1 && !GameManager.gameOver)     //背景が-10より小さくなったら実行
        {
            transform.position = new Vector3(0, 0, 0);  //背景を初期位置に戻す
            ScoreManager.Score += 1;       //スコア加算
            if (ScoreManager.Score % 100 == 0)  //100で割り切れたら実行
            {
                ScrollSpeed = ScrollSpeed - 0.1f;
                moveCloud.vecx = moveCloud.vecx - 0.1f;
            }
        }
    }
}
