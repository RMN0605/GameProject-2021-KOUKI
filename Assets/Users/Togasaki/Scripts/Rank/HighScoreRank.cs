using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;
using UnityEngine.UI;

public class HighScoreRank : MonoBehaviour
{

    [SerializeField, Header("Content")]
    private GameObject cntObj;

    private void Start()
    {
        LoadHighScore();
    }

    public void LoadHighScore()
    {
        Text[] rankArray = cntObj.GetComponentsInChildren<Text>();

        //順位1～30
        for (int i = 0; i < 30; i++)
        {
            int rank = i + 1;
            rankArray[i * 3].text = rank.ToString();
        }

        //データストア"HighScore"から検索
        NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject>("HighScore");
        query.OrderByDescending("Score");

        query.FindAsync((List<NCMBObject> objList, NCMBException e) =>
        {
            //検索成功したら
            if (e == null)
            {
                for (int i = 0; i < 30; i++)
                {
                    if(objList.Count > i)
                    {
                        //プレイヤー名を入力
                        rankArray[1 + (i * 3)].text = objList[i]["Name"].ToString();

                        //ハイスコアを入力
                        rankArray[2 + (i * 3)].text = objList[i]["Score"].ToString() + "M";
                    }
                    else
                    {
                        break;
                    }
                }
            }
        });
    }

}
