using Data.ChartData;
using UnityEngine;
using UnityEngine.UI;
using UtilityCode.Singleton;
using GlobalData = Scenes.DontDestoryOnLoad.GlobalData;
namespace Manager
{
    public class WebManager : MonoBehaviourSingleton<WebManager>
    {
        public ChartData ChartData
        {
            get => AssetManager.Instance.chartData;
            set
            {
                AssetManager.Instance.chartData = value;
                GlobalData.Instance.score.Reset();
                GlobalData.Instance.score.tapCount = value.globalData.tapCount;
                GlobalData.Instance.score.holdCount = value.globalData.holdCount;
                GlobalData.Instance.score.dragCount = value.globalData.dragCount;
                GlobalData.Instance.score.flickCount = value.globalData.flickCount;
                GlobalData.Instance.score.fullFlickCount = value.globalData.fullFlickCount;
                GlobalData.Instance.score.pointCount = value.globalData.pointCount;
                UIManager.Instance.musicName.text = value.metaData.musicName;
                UIManager.Instance.level.text = value.metaData.chartLevel;
                TextManager.Instance.Init(value.texts);
            }

        }
        public AudioClip MusicClip
        {
            get => AssetManager.Instance.musicPlayer.clip;
            set => AssetManager.Instance.musicPlayer.clip = value;
        }
        public Image Background
        {
            get => AssetManager.Instance.background;
            set => AssetManager.Instance.background = value;
        }
        private void Start()
        {
            ChartData = GlobalData.Instance.chartData;
            MusicClip = GlobalData.Instance.clip;
            Background.sprite = GlobalData.Instance.currentCp;
        }
    }
}
