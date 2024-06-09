using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay_Retry : Public_Button
{
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(() => Loading_Controller.Instance.SetLoadSceneByName("Gameplay").StartLoad());
    }
}
