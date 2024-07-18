using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleBGM : MonoBehaviour
{
    public GameObject bgm;
    void Start()
    {
        DontDestroyOnLoad(bgm);
    }
}
