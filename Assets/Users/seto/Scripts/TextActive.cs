﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextActive : MonoBehaviour
{
    void Update()
    {
        clik();
    }
    private void clik()
    {
        if(Input.GetMouseButtonDown(0))
        {
            this.gameObject.SetActive (false);
        } 
    }
}
