using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : Singleton<Managers>
{
    void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this);
    }
}