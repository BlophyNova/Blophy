using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Settings_IsAutoplay : Public_Button
{
    public TextMeshProUGUI thisText;
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(() =>
        {
            if (GlobalData.Instance.isAutoplay)
            {
                GlobalData.Instance.isAutoplay = false;
                thisText.text = "自动播放: 关";
            }
            else
            {
                GlobalData.Instance.isAutoplay = true;
                thisText.text = "自动播放: 开";
            }
        });
    }
}
