using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoWall : MonoBehaviour
{
    [SerializeField, Header("���C���J����")]
    private Camera _mainCamera;

    /// <summary>
    /// ���C���J�����̍����[
    /// </summary>
    private Vector3 topLeft;

    /// <summary>
    /// �������ǂ��̃u���b�N�Ȃ̂�
    /// </summary>
    public bool _left = false;
    public bool _right = false;
    public bool _top = false;

    void Start()
    {
        topLeft = _mainCamera.ScreenToWorldPoint(Vector3.zero);

        //���̏ꍇ
        if(_left)
        {
            if (topLeft.x < -15)
            {
                gameObject.transform.position = new Vector3(-12.5f, 0, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(topLeft.x - 0.5f, 0, 0);
            }
        }
        //�E�̏ꍇ
        else if(_right)
        {
            if (topLeft.x < -15)
            {
                gameObject.transform.position = new Vector3(12.5f, 0, 0);
            }
            else
            {
                gameObject.transform.position = new Vector3(-topLeft.x + 0.5f, 0, 0);
            }
        }
        //��̏ꍇ
        else if(_top)
        {
            gameObject.transform.position = new Vector3(0, -topLeft.y + 0.5f, 0);
        }

    }

}
