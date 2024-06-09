using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.Chart;
using System.Diagnostics;
using Newtonsoft.Json;
public class TestChart : MonoBehaviourSingleton<TestChart>
{
    public ChartData chartData;
    public AudioClip clip;

    private void Start()
    {
        string chart = Resources.Load<TextAsset>("MusicPack/Tutorial/Base/ChartFile/Chart").text;
        chartData = JsonConvert.DeserializeObject<ChartData>(chart);
    }
    private void Update()
    {
    }
}
