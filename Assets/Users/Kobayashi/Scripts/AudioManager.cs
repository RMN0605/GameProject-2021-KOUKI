using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///<summary>
///BGMとSEの管理をするマネージャー
///</summary>
///
[System.Serializable]
public class Sound
{
    private AudioSource source;
    //=================================================================================
    //オーディオ詳細
    //=================================================================================
    public string Name; //オーディオの名前
    public AudioClip Clip; //オーディオ保管
    [Range(0f, 1f)] public float Volume = 0.7f; //オーディオのボリューム
    [Range(0f, 1.5f)] public float Pitch = 1f; //オーディオのピッチ

    [Range(0f, 0.5f)] public float RandomVolume = 0.1f;
    [Range(0f, 0.5f)] public float RandomPicth = 0.1f;

    public bool loop = false; //ループのチェック

    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = Clip;
        source.loop = loop;
    }

    public void Play() //プレイメソッド
    {
        source.volume = Volume * (1 + Random.Range(-RandomVolume / 2f, RandomVolume / 2f)); //ランダムでボリュームを変える
        source.pitch = Pitch * (1 + Random.Range(-RandomPicth / 2f, RandomPicth / 2f)); //ランダムでピッチを変える
        source.Play();
    }
    public void Stop() //ストップメソッド
    {
        source.Stop();
    }
}
public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] Sound[] sounds;
    //=================================================================================
    //初期化
    //=================================================================================

    void Awake()
    {
        if (instance != null)
        {
            if (instance != this)
            {
                Destroy(this.gameObject);
            }
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        //セットサウンドにひとつずつ格納
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject _go = new GameObject("Sound_" + i + "_" + sounds[i].Name);
            sounds[i].SetSource(_go.AddComponent<AudioSource>());
        }
    }
    public void PlaySound(string _Name) //プレイサウンドメソッド
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].Name == _Name)
            {
                sounds[i].Play(); //選んだ曲があった場合
                return;
            }
            else
            {
                Debug.Log(_Name + "がありません");//なかった場合
            }
        }

    }
    public void StopSound(string _Name) //ストップサウンドメソッド
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].Name == _Name)
            {
                sounds[i].Stop(); //選んだ曲があった場合
                return;
            }
            else
            {
                Debug.Log(_Name + "がありません");//なかった場合
            }
        }

    }
}