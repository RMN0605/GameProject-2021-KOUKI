using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCloud : MonoBehaviour
{
    /// <summary>
    /// 雲の速さ
    /// </summary>
    public float vecx = -1;
    private void FixedUpdate()
    {
        this.transform.Translate( vecx * Time.deltaTime, 0, 0);  //オブジェクトを移動
    }
}
