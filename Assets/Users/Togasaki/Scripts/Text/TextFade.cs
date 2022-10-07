using UnityEngine.UI;
using UnityEngine;

public class TextFade : MonoBehaviour
{
    [SerializeField, Header("フェードさせるテキスト")]
    private Text fadeText;

    [SerializeField, Header("フェードさせる速さ")]
    private float _fadeSpeed = 1;

    /// <summary>
    /// フェードインアウトフラグ
    /// </summary>
    private bool _fadeFlag = true;


    // Update is called once per frame
    void FixedUpdate()
    {
        TextFadeFunc();
    }

    private void TextFadeFunc()
    {
        Color color = fadeText.material.GetColor("_Color");

        if (_fadeFlag)      //フェードアウト
        {
            color.a -= _fadeSpeed;
            Mathf.Clamp01(color.a);
            if (color.a <= 0)
            {
                _fadeFlag = false;
            }
            fadeText.material.SetColor("_Color", color);
        }
        else                //フェードイン
        {
            if (color.a > 0.4f)
            {
                color.a += _fadeSpeed * 0.5f;
            }
            else
            {
                color.a += _fadeSpeed;
            }
            Mathf.Clamp01(color.a);
            if (color.a >= 1)
            {
                _fadeFlag = true;
            }
            fadeText.material.SetColor("_Color", color);

        }

    }

}
