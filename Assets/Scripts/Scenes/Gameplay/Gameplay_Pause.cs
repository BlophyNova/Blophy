using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gameplay_Pause : Public_Button
{
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(() =>
        {
            ProgressManager.Instance.PausePlay();
            SpeckleManager.Instance.enabled = false;
            UIManager.Instance.pauseCanvas.gameObject.SetActive(true);
        });
    }
}
