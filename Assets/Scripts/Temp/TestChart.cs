using Data.ChartData;
using Newtonsoft.Json;
using UnityEngine;
using UtilityCode.Singleton;
namespace Temp
{
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
}
