using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Settings_OffsetSubtract : Public_Button
{
    public TextMeshProUGUI offsetText;
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(() =>
        {
            GlobalData.Instance.offset -= .005f;
            offsetText.text = $"{GlobalData.Instance.offset * 1000:F0}";
        });
    }
}
