using System;
using System.IO;
using Newtonsoft.Json;
using Scenes.DontDestroyOnLoad;
using UnityEngine;
using UtilityCode.Singleton;
namespace Data.ArchiveData
{
    public class ArchiveData : MonoBehaviourSingleton<ArchiveData>
    {
        public Archive archive = new Archive();
        private void Start()
        {
            if (File.Exists($"{Application.persistentDataPath}/Archive.HuaWaterED"))
            {
                archive = JsonConvert.DeserializeObject<Archive>(File.ReadAllText($"{Application.persistentDataPath}/Archive.HuaWaterED"));
            }
            else
            {
                archive.chapterArchives = new ChapterArchive[GlobalData.Instance.chapters.Length];
                for (int i = 0; i < archive.chapterArchives.Length; i++)
                {
                    archive.chapterArchives[i] = new ChapterArchive
                    {
                        musicArchive = new MusicArchive[GlobalData.Instance.chapters[GlobalData.Instance.currentChapter].musicPath.Length]
                    };
                    for (int j = 0; j < archive.chapterArchives[i].musicArchive.Length; j++)
                    {
                        archive.chapterArchives[i].musicArchive[j] = new MusicArchive();
                    }
                }
                SaveArchive();
            }
        }
        public void SaveArchive()
        {
            File.WriteAllText($"{Application.persistentDataPath}/Archive.HuaWaterED", JsonConvert.SerializeObject(archive));
        }
    }
    [Serializable]
    public class Archive
    {
        public ChapterArchive[] chapterArchives;
    }
    [Serializable]
    public class ChapterArchive
    {
        public MusicArchive[] musicArchive;
    }
    [Serializable]
    public class MusicArchive
    {
        public int scoreEasy;
        public int scoreNormal;
        public int scoreHard;
        public int this[string hard]
        {
            get => hard switch
            {
                "Green" => scoreEasy,
                "Yellow" => scoreNormal,
                "Red" => scoreHard,
                _ => throw new Exception("如果你看到这条消息,请截图并在群中@MojaveHao/Niubility748/HuaWaterED中的任意一位反馈\n" +
                    "存档系统读取方法出错,难度未找到")
            };
            set
            {
                switch (hard)
                {
                    case "Green":
                        scoreEasy = value;
                        break;
                    case "Yellow":
                        scoreNormal = value;
                        break;
                    case "Red":
                        scoreHard = value;
                        break;
                }
            }
        }
    }
}