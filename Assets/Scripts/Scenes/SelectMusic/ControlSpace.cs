using System.Collections;
using Data.ChartData;
using Newtonsoft.Json;
using Scenes.PublicScripts;
using UnityEngine;
using UnityEngine.UI;
using GlobalData = Scenes.DontDestroyOnLoad.GlobalData;
namespace Scenes.SelectMusic
{
    public class ControlSpace : PublicControlSpace
    {
        public static ControlSpace instance;
        private void Awake() => instance = this;
        public string[] musics;
        public Image musicPrefab;
        private static new IEnumerator Send()
        {
            ResourceRequest rawChart = Resources.LoadAsync<TextAsset>($"MusicPack/Chapters/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/Charts/{GlobalData.Instance.currentHard}/Chart");
            yield return rawChart;
            TextAsset rawChartTex = rawChart.asset as TextAsset;
            //GlobalData.Instance. = JsonConvert.DeserializeObject<ChartData>(chart);
            ChartData chart = JsonConvert.DeserializeObject<ChartData>(rawChartTex!.text); // 这里不是null才能读text attr所以干脆加个断言了
            GlobalData.Instance.chartData = chart;
            UIManager.Instance.SelectMusic(chart.metaData.musicName, chart.metaData.musicWriter, chart.metaData.chartWriter, chart.metaData.artWriter);

            ResourceRequest currentPlayBackground = Resources.LoadAsync<Sprite>($"MusicPack/Chapters/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/Backgrounds/CP");
            yield return currentPlayBackground;
            GlobalData.Instance.currentCp = currentPlayBackground.asset as Sprite;

            ResourceRequest currentCover = Resources.LoadAsync<Sprite>($"MusicPack/Chapters/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/Backgrounds/CPH");
            yield return currentCover;
            GlobalData.Instance.currentCph = currentCover.asset as Sprite;

            ResourceRequest clip = Resources.LoadAsync<AudioClip>($"MusicPack/Chapters/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.currentMusic}/Music/Music");
            yield return clip;
            GlobalData.Instance.clip = clip.asset as AudioClip;


        }
        private void UploadSyncMusic()
        {
            GlobalData.Instance.currentMusic = musics[currentElementIndex];
        }
        private void DownloadSyncMusic()
        {
            currentElement = GlobalData.Instance.currentMusicIndex;
            currentElement = allElementDistance[elementCount - 1 - currentElementIndex];
            GlobalData.Instance.currentMusic = musics[currentElementIndex];
        }
        protected override void OnStart()
        {
            musics = GlobalData.Instance.chapters[GlobalData.Instance.currentChapter].musicPath;
            int currentChapterMusicCount = GlobalData.Instance.chapters[GlobalData.Instance.currentChapter].musicPath.Length;
            elementCount = currentChapterMusicCount;
            currentElement = 1;
            InitAllElementDistance();
            for (int i = 0; i < currentChapterMusicCount; i++)
            {
                Instantiate(musicPrefab, transform).sprite = Resources.Load<Sprite>($"MusicPack/{GlobalData.Instance.currentChapter}/{GlobalData.Instance.chapters[GlobalData.Instance.currentChapter].musicPath[i]}/Background/CPH");
            }
            DownloadSyncMusic();
            StartCoroutine(Send());
            StartCoroutine(ReturnLastTimeCancelMusic());

        }
        private IEnumerator ReturnLastTimeCancelMusic()
        {
            yield return new WaitForEndOfFrame();
            verticalBar.value = allElementDistance[elementCount - 1 - currentElementIndex];
        }

        private Vector2 startPoint;
        private Vector2 endPoint;
        protected override void LargeImageUpdate()
        {
            if( Input.touchCount <= 0 )
                return;
            Touch touch = Input.GetTouch(0);
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPoint = touch.position;
                    break;
                case TouchPhase.Ended:
                {
                    endPoint = touch.position;
                    float deltaY = (endPoint - startPoint).y;
                    currentElement = deltaY switch
                    {
                        > 300 when currentElement + 1 < elementCount => allElementDistance[elementCount - 1 - ++currentElementIndex],
                        < -300 when currentElement - 1 >= 0 => allElementDistance[elementCount - 1 - --currentElementIndex],
                        _ => currentElement
                    };
                    UploadSyncMusic();
                    StartCoroutine(Send());
                    StartCoroutine(Lerp());
                    break;
                }
            }
        }
    }
}
