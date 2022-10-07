using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject Image1;
    [SerializeField] GameObject Image2;
    [SerializeField] GameObject TText;

    Image image;
     private int counter = 0;
    //const int counter3 = 3;
        void Start()
        {
            Invoke("DelayMethod", 0.1f);
        }
    void Update()
    {
       Clik();
       
    }
    void Clik()
    {
         if(Input.GetMouseButtonDown(0))
        {
            counter++;
            
            HighLight1();
            HighLight2(); 
            TTextclik();  
        }
        
    }

    void DelayMethod()
    {
        Time.timeScale = 0;
    }

    void HighLight1()
    {
        Image1.SetActive (false);
    }

    void HighLight2()
    {
        if(Image2.activeSelf == false)
        {
            Image2.SetActive(true);
        }  
        else
        {
            Image2.SetActive(false);
            Time.timeScale = 1;
            //Destroy(Image2);
        }
    }

     void TTextclik()
    {
        if(counter >= 2)
        {
            TText.SetActive(false);
        }
       
    }
}
