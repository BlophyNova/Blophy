using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SelectChapter_Start : Public_Button
{
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(() => Loading_Controller.Instance.SetLoadSceneByName("SelectMusic").StartLoad());
    }
}
