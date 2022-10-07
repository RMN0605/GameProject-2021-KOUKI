using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class GameManager : MonoBehaviour
{
    public ScoreManager scoreManager;           //スコアマネージャのスクリプト参照
    /// <summary>
    /// ゲームオーバーフラグ
    /// </summary>
    public static bool gameOver = false;
    public static bool practiceGameOver = false;
    /// <summary>
    /// ゲームオーバーオブジェクト
    /// </summary>
    private static GameObject gameOverObj;

    private static GameObject highScoreText;

    /// <summary>
    /// 既に名前が存在しているかどうかのフラグ
    /// </summary>
    private static bool _isExist = false;

    private void Awake()
    {
        _isExist = false;
        gameOverObj = GameObject.FindGameObjectWithTag("GameOverObj");
        highScoreText = GameObject.FindGameObjectWithTag("HighScoreObj");
        highScoreText.SetActive(false);
        gameOverObj.SetActive(false);
        gameOver = false;
    }

    /// <summary>
    /// ゲームオーバー
    /// </summary>
    public static void GameOverFunc()
    {
        gameOver = true;
        gameOverObj.SetActive(true);

        //ハイスコアを更新していた場合
        if (PlayerPrefs.GetFloat("myScore") < ScoreManager.Score)
        {         
            highScoreText.SetActive(true);

            //スコア保存
            PlayerPrefs.SetFloat("myScore", ScoreManager.Score);
            PlayerPrefs.Save();

            //データストア"HighScore"から検索
            NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
            
            query.OrderByDescending("Score");

            query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
            {

                //検索成功したら
                if (e == null)
                {
                    for (int i = 0; i < objList.Count; i++)
                    {
                        //同じオブジェクトIDが既にあるかどうか
                        if (objList[i]["Name"].ToString() == PlayerPrefs.GetString("playerName"))
                        {
                            _isExist = true;
                        }
                    }

                    //同じオブジェクトIDが既にある場合は名前とハイスコアを書き換える
                    if (_isExist)
                    {
                        NCMBObject obj = new NCMBObject("HighScore");
                        obj.ObjectId = PlayerPrefs.GetString("myObjectID");
                        obj.FetchAsync((NCMBException e) =>
                        {
                            if (e != null)
                            {
                                //エラー処理
                            }
                            else
                            {
                                //成功時の処理                                
                                obj["Name"] = PlayerPrefs.GetString("playerName");  //プレイヤー名
                                obj["Score"] = ScoreManager.Score;                  //新しいスコア
                                obj.SaveAsync();
                            }
                        });

                    }
                    else
                    {
                        //ない場合はNCMBのデータストアにスコアを登録
                        NCMBObject obj = new NCMBObject("HighScore");
                        obj["Name"] = PlayerPrefs.GetString("playerName");      //プレイヤーネーム
                        obj["Score"] = ScoreManager.Score;                      //スコア

                        //サーバーに書き込み
                        obj.SaveAsync((NCMBException e) =>
                        {

                            if (e != null)
                            {
                            //エラー処理
                            }
                            else
                            {
                            //成功時の処理
                            PlayerPrefs.SetString("myObjectID", obj.ObjectId);
                            }

                        });

                    }

                }
            });
        }
    }

}
