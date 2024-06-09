using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Title_TouchToStart : Public_Button
{
    private void Start()
    {
        SceneManager.LoadSceneAsync("Loading", LoadSceneMode.Additive).completed += (AsyncOperation a) =>
        Loading_Controller.Instance.SetLoadSceneByName("SelectChapter");

        thisButton.onClick.AddListener(() => Loading_Controller.Instance.StartLoad());
    }
}
