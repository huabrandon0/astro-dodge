﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    protected InputManager() { }

    public void HelloWorld()
    {
        Debug.Log("Hello World!");
    }
}
