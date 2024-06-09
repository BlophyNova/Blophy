using Blophy.Chart;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class SelectMusic_ControlSpace : Public_ControlSpace
{
    public static SelectMusic_ControlSpace Instance;
    private void Awake() => Instance = this;
    public string[] musics;
    public Image musicPrefab;
    public new IEnumerator Send()
    {
        ResourceRequest rawChart = Resources.LoadAsync<TextAsset>($"MusicPack/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/ChartFile/{GlobalData.Instance.currentHard}/Chart");
        yield return rawChart;
        TextAsset rawChartTex = rawChart.asset as TextAsset;
        //GlobalData.Instance. = JsonConvert.DeserializeObject<ChartData>(chart);
        ChartData chart = JsonConvert.DeserializeObject<ChartData>(rawChartTex.text);
        GlobalData.Instance.chartData = chart;
        SelectMusic_UIManager.Instance.SelectMusic(chart.metaData.musicName, chart.metaData.musicWriter, chart.metaData.chartWriter, chart.metaData.artWriter);

        ResourceRequest currentCP = Resources.LoadAsync<Sprite>($"MusicPack/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/Background/CP");
        yield return currentCP;
        GlobalData.Instance.currentCP = currentCP.asset as Sprite;

        ResourceRequest currentCPH = Resources.LoadAsync<Sprite>($"MusicPack/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/Background/CPH");
        yield return currentCPH;
        GlobalData.Instance.currentCPH = currentCPH.asset as Sprite;

        ResourceRequest clip = Resources.LoadAsync<AudioClip>($"MusicPack/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/Music/Music");
        yield return clip;
        GlobalData.Instance.clip = clip.asset as AudioClip;


    }
    void UploadSyncMusicIndex()
    {
        GlobalData.Instance.currentMusicIndex = currentElementIndex;
        GlobalData.Instance.currentMusic = musics[currentElementIndex];
    }
    void DownloadSyncMusicIndex()
    {
        currentElementIndex = GlobalData.Instance.currentMusicIndex;
        currentElement = allElementDistance[elementCount - 1 - currentElementIndex];
        GlobalData.Instance.currentMusic = musics[currentElementIndex];
    }
    protected override void OnStart()
    {
        musics = GlobalData.Instance.chapters[GlobalData.Instance.currentChapterIndex].musicPath;
        int currentChapterMusicCount = GlobalData.Instance.chapters[GlobalData.Instance.currentChapterIndex].musicPath.Length;
        elementCount = currentChapterMusicCount;
        currentElement = 1;
        InitAllElementDistance();
        for (int i = 0; i < currentChapterMusicCount; i++)
        {
            Instantiate(musicPrefab, transform).sprite = Resources.Load<Sprite>($"MusicPack/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.chapters[GlobalData.Instance.currentChapterIndex].musicPath[i]}/Background/CPH");
        }
        DownloadSyncMusicIndex();
        StartCoroutine(Send());
        StartCoroutine(ReturnLastTimeCancelMusic());

    }
    IEnumerator ReturnLastTimeCancelMusic()
    {
        yield return new WaitForEndOfFrame();
        verticalBar.value = allElementDistance[elementCount - 1 - currentElementIndex];
    }

    Vector2 startPoint;
    Vector2 endPoint;
    protected override void LargeImageUpdate()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPoint = touch.position;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                endPoint = touch.position;
                float deltaY = (endPoint - startPoint).y;
                if (deltaY > 300 && currentElementIndex + 1 < elementCount)
                {
                    currentElement = allElementDistance[elementCount - 1 - ++currentElementIndex];
                }
                else if (deltaY < -300 && currentElementIndex - 1 >= 0)
                {
                    currentElement = allElementDistance[elementCount - 1 - --currentElementIndex];
                }
                UploadSyncMusicIndex();
                StartCoroutine(Send());
                StartCoroutine(Lerp());
            }
        }
    }
}
