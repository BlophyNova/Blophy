using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMusic_Start : Public_Button
{
    private void Start()
    {
        thisButton.onClick.AddListener(() => Loading_Controller.Instance.SetLoadSceneByName("Gameplay").StartLoad());
    }
}
