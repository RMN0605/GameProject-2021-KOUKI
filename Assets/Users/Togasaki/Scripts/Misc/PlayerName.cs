using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NCMB;

public class PlayerName : MonoBehaviour
{
    [SerializeField, Header("NamePanel")]
    private GameObject _namePanelObj;

    [SerializeField, Header("ExistPanel")]
    private GameObject _existPanelObj;

    [SerializeField, Header("上に表示されるテキスト")]
    private Text _playerNameText;

    [SerializeField, Header("プレイヤーが実際に入力するテキスト")]
    private Text _namePanelText;

    /// <summary>
    /// 既に名前が存在しているかどうかのフラグ
    /// </summary>
    private bool _isExist = false;


    private void Awake()
    {
        //playerNameセーブスロットがあればそれを読み込み自分を非表示
        if (PlayerPrefs.HasKey("playerName"))
        {
            _playerNameText.text = PlayerPrefs.GetString("playerName");
            _namePanelObj.SetActive(false);
        }
    }

    public void isFirstPlay()
    {
        if (PlayerPrefs.HasKey("playerName"))
        {
            _namePanelObj.SetActive(false);
        }
    }

    /// <summary>
    /// 名前のセーブ
    /// </summary>
    public void SaveName()
    {
        //ここに名前の条件を追加
        if (!string.IsNullOrWhiteSpace(_namePanelText.text))
        {
            _isExist = false;

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
                        //プレイヤー名が既に使われていないか検索
                        if (objList[i]["Name"].ToString() == _namePanelText.text.ToString())
                        {
                            _isExist = true;
                        }
                    }

                    //もし名前が既に存在していたらもう一度
                    if(_isExist)
                    {
                        _namePanelText.text = "";
                        _existPanelObj.SetActive(true);
                    }
                    else
                    {
                        if(PlayerPrefs.HasKey("playerName"))
                        {
                            PlayerPrefs.SetString("playerName", _namePanelText.text);
                            PlayerPrefs.Save();

                            //サーバー上の自分の名前を書き換え
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
                                    obj.SaveAsync();
                                }
                            });
                        }
                        else
                        {
                            PlayerPrefs.SetString("playerName", _namePanelText.text);
                            PlayerPrefs.Save();

                        }

                        _playerNameText.text = _namePanelText.text;
                        _namePanelObj.SetActive(false);

                    }

                }
            });


        }
    }

}
