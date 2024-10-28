using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.ChartData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using WebSocketSharp;

public class OK : MonoBehaviour
{
    public TMP_InputField ipAddress;
    public Button ok;
    public TMP_Text connectionInfo;

    private void Start()
    {
        ok.onClick.AddListener(() =>
        {
            using (var ws = new WebSocket($"ws://{ipAddress.text}:1286/"))
            {
                ws.Connect();
                ws.SendAsync("摩西摩西，我是打谱器，制谱器在吗？",b =>Debug.Log(b));
            }
        });

        
    }
}