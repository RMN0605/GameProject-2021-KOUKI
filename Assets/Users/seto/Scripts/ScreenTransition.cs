/// <summary>
/// 著・瀨戸大輝
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    //メニュー画面からタイトルに遷移
    public void PlayTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    //タイトル画面からメニュー画面に遷移する
    public void GameStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
    //メニュー画面からスコアアタックに遷移
    //memo　TestSceneを遷移させたいシーンに入れ替える。
    public void PlayScoreAttack()
    {
        SceneManager.LoadScene("ScoreAttackScene");
    }
    //メニュー画面からチュートリアルに遷移
    //memo　TestSceneを遷移させたいシーンに入れ替える。
    public void PlayTutorial()
    {
         SceneManager.LoadScene("TutorialScene");
    }
    //メニュー画面からハイスコアに遷移
    public void PlayHighScore()
    {
        SceneManager.LoadScene("HighScoreScene");
    }
    //メニュー画面から訓練場に遷移
    public void PlayTraining()
    {
        SceneManager.LoadScene("TrainingScene");
    }
}
