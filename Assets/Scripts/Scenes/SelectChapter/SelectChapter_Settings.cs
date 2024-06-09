using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectChapter_Settings : Public_Button
{
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(() => Loading_Controller.Instance.SetLoadSceneByName("Settings").StartLoad());
    }
}
