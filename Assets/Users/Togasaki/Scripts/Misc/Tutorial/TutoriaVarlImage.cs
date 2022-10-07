using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutoriaVarlImage : MonoBehaviour
{
    [SerializeField, Header("âÊëú")]
    private Image[] panels;

    /// <summary>
    /// ï\é¶Ç∑ÇÈâÊëúî‘çÜ
    /// </summary>
    int panelIndex = 0;

    private void Start()
    {
        foreach(Image img in panels)
        {
            img.gameObject.SetActive(false);
        }

        panels[0].gameObject.SetActive(true);

    }

    private void Update()
    {
        Click();
    }

    void Click()
    {
        if (Input.GetMouseButtonDown(0))
        {
            panels[panelIndex].gameObject.SetActive(false);
            
            panelIndex++;

            if (panelIndex == panels.Length)
            {
                SceneManager.LoadScene("MenuScene");
            }
            else
            {
                panels[panelIndex].gameObject.SetActive(true);

            }

        }
    }


}
