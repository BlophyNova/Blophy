using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.ChartData;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class OK : MonoBehaviour
{
    public TMP_InputField ipAddress;
    public Button ok;
    public TMP_Text connectionInfo;
    private void Start()
    {
        IThenStartup startup = (IThenStartup)new GameObject().AddComponent(Type.GetType("HuaWaterED.ThenStartup"));
        if (startup == null)
        {
            Debug.Log("未找到网络模块，此版本无互联网访问能力");
            return;
        }
        ok.onClick.AddListener(() => startup.ClientInit(ipAddress.text));
    }
}