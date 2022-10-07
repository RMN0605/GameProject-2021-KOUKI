using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteCloud : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < -15) //オブジェクトの消去
        {
            Destroy(this.gameObject);
        }
    }
}
