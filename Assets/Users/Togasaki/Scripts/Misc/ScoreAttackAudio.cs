using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAttackAudio : MonoBehaviour
{
    private AudioManager audioManagerS;  //�I�[�f�B�I�}�l�[�W���[�X�N���v�g�̎Q��
    // Start is called before the first frame update
    void Start()
    {
        audioManagerS = AudioManager.instance;   //���������߂ɕK�v
        
        audioManagerS.PlaySound("ScoreAttackBGM");
    }
}
