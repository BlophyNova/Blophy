using Blophy.Chart;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SelectMusic_Settings : Public_Button
{
    private void Start()
    {
        GlobalData.Instance.WhereToEnterSettings = "SelectMusic";
        thisButton.onClick.AddListener(() => Loading_Controller.Instance.SetLoadSceneByName("Settings").StartLoad());
    }
}
