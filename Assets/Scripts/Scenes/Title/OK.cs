using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Data.ChartData;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using WebSocketSharp;

public class OK : MonoBehaviour
{
    public TMP_InputField ipAddress;
    public Button ok;
    public TMP_Text connectionInfo;
    public WebSocket ws;
    private void Start()
    {
        ok.onClick.AddListener(() =>StartCoroutine(ConnectEdit()));
    }

    IEnumerator ConnectEdit()
    {
        connectionInfo.text = "正在寻找制谱器中";
        yield return new WaitForEndOfFrame();
        ws = new($"ws://{ipAddress.text}:1286/");
        
        ws.OnOpen += (sender, args) =>StartCoroutine(Jump2Gameplay());
        //ws.OnError += (sender, args) =>StartCoroutine(ConnectFailed());
        ws.Connect();
    }

    IEnumerator ConnectFailed()
    {
        connectionInfo.text = "阿巴阿巴，连不上制谱器呀";
        yield return new WaitForEndOfFrame();
    }
    IEnumerator Jump2Gameplay()
    {
        connectionInfo.text = "连接成功！即将跳转！";
        StartCoroutine(PingPong());
        yield return new WaitForSeconds(3);
        //SceneManager.LoadScene("GamePlay", LoadSceneMode.Single);
    }

    IEnumerator PingPong()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (!ws.Ping())
            {
                ws.Close();
            }
            
            
        }
    }

    private void OnApplicationQuit()
    {
        ws.Close();
    }
}