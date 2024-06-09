using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class ArchiveData : MonoBehaviourSingleton<ArchiveData>
{
    public Archive archive = new();
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
                archive.chapterArchives[i] = new()
                {
                    musicArchive = new MusicArchive[GlobalData.Instance.chapters[GlobalData.Instance.currentChapterIndex].musicPath.Length]
                };
                for (int j = 0; j < archive.chapterArchives[i].musicArchive.Length; j++)
                {
                    archive.chapterArchives[i].musicArchive[j] = new();
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
    public int score_Green;
    public int score_Yellow;
    public int score_Red;
    public int this[string hard]
    {
        get => hard switch
        {
            "Green" => score_Green,
            "Yellow" => score_Yellow,
            "Red" => score_Red,
            _ => throw new Exception("如果你看到这条消息，请截图发给花水终，这有助于我们改进游戏！\n" +
                "存档系统出错！错误点：读取方法出错！\n" +
                "错误类型：没有找到对应的难度")
        };
        set
        {
            switch (hard)
            {
                case "Green":
                    score_Green = value;
                    break;
                case "Yellow":
                    score_Yellow = value;
                    break;
                case "Red":
                    score_Red = value;
                    break;
            }
        }
    }
}
