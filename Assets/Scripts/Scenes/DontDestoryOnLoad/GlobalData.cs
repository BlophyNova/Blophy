using Beebyte.Obfuscator;
using Blophy.Chart;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static UnityEngine.Camera;
using VM = ValueManager;

public class GlobalData : MonoBehaviourSingleton<GlobalData>
{
    public Chapter[] chapters;
    public string currentChapter;
    public int currentChapterIndex;
    public string currentMusic;
    public int currentMusicIndex;
    public string currentHard;
    public ChartData chartData;
    public AudioClip clip;
    public Sprite currentCP;
    public Sprite currentCPH;
    public Grade score;
    public bool isAutoplay = true;
    public float offset;
    public int ScreenWidth => main.pixelWidth;
    public int ScreenHeight => main.pixelHeight;
    public string WhereToEnterSettings;
    protected override void OnAwake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        Application.targetFrameRate = 9999;
    }
}
[Serializable]
public class Grade
{
    public int tapPerfect;
    public int holdPerfect;
    public int dragPerfect;
    public int flickPerfect;
    public int fullFlickPerfect;
    public int pointPerfect;

    public int tapGood;
    public int holdGood;
    public int pointGood;
    public int tapEarly_good;
    public int holdEarly_good;
    public int pointEarly_good;
    public int tapLate_good;
    public int holdLate_good;
    public int pointLate_good;

    public int tapBad;
    public int tapEarly_bad;
    public int tapLate_bad;
    public int pointBad;
    public int pointEarly_bad;
    public int pointLate_bad;

    public int tapMiss;
    public int holdMiss;
    public int dragMiss;
    public int flickMiss;
    public int fullFlickMiss;
    public int pointMiss;


    public int Perfect => tapPerfect + holdPerfect + dragPerfect + flickPerfect + fullFlickPerfect + pointPerfect;

    public int Good => tapGood + holdGood + pointGood;
    public int Early_good => tapEarly_good + holdEarly_good + pointEarly_good;
    public int Late_good => tapLate_good + holdLate_good + pointLate_good;

    public int Bad => tapBad + pointBad;
    public int Early_bad => tapEarly_bad + pointEarly_bad;
    public int Late_bad => tapLate_bad + pointLate_bad;

    public int Miss => tapMiss + holdMiss + dragMiss + flickMiss + fullFlickMiss + pointMiss;
    public int combo;
    public int Combo
    {
        get => combo;
        set
        {
            combo = value;
            maxCombo = maxCombo >= combo ? maxCombo : combo;
        }
    }
    public int maxCombo;

    public int tapCount;
    public int holdCount;
    public int dragCount;
    public int flickCount;
    public int fullFlickCount;
    public int pointCount;
    public int JudgedTapCount => tapPerfect + tapGood + tapBad + tapMiss;
    public int JudgedHoldCount => holdPerfect + holdGood + holdMiss;
    public int JudgedDragCount => dragPerfect + dragMiss;
    public int JudgedFlickCount => flickPerfect + flickMiss;
    public int JudgedFullFlickCount => fullFlickPerfect + fullFlickMiss;
    public int JudgedPointCount => pointPerfect + pointGood + pointMiss;
    public int NoteCount => tapCount + holdCount + dragCount + flickCount + fullFlickCount + pointCount;
    public float Accuracy => (Perfect + Good * VM.Instance.goodJudgePercent) / NoteCount;

    public float delta = -1;
    public float Delta
    {
        get
        {
            if (delta < 0)
            {
                delta = 350000f /
                (VM.Instance.tapWeight * tapCount +
                VM.Instance.holdWeight * holdCount +
                VM.Instance.dragWeight * dragCount +
                VM.Instance.flickWeight * flickCount +
                VM.Instance.fullFlickWeight * fullFlickCount +
                VM.Instance.pointWeight * pointCount);
            }
            return delta;
        }
    }
    public float score;
    public float Score
    {
        get
        {
            float result = 500000f * Accuracy +
              150000f * maxCombo / NoteCount +
            Delta * (VM.Instance.tapWeight * tapPerfect) +
            Delta * (VM.Instance.holdWeight * holdPerfect) +
            Delta * (VM.Instance.dragWeight * dragPerfect) +
            Delta * (VM.Instance.flickWeight * flickPerfect) +
            Delta * (VM.Instance.fullFlickWeight * fullFlickPerfect) +
            Delta * (VM.Instance.pointWeight * pointPerfect);
            return result;
        }
    }
    public void Reset()
    {
        tapPerfect = 0;
        holdPerfect = 0;
        dragPerfect = 0;
        flickPerfect = 0;
        fullFlickPerfect = 0;
        pointPerfect = 0;
        tapGood = 0;
        holdGood = 0;
        pointGood = 0;
        tapEarly_good = 0;
        holdEarly_good = 0;
        pointEarly_good = 0;
        tapLate_good = 0;
        holdLate_good = 0;
        pointLate_good = 0;
        tapBad = 0;
        tapEarly_bad = 0;
        tapLate_bad = 0;
        pointBad = 0;
        pointEarly_bad = 0;
        pointLate_bad = 0;
        tapMiss = 0;
        holdMiss = 0;
        dragMiss = 0;
        flickMiss = 0;
        fullFlickMiss = 0;
        pointMiss = 0;
        combo = 0;
        maxCombo = 0;
        tapCount = 0;
        holdCount = 0;
        dragCount = 0;
        flickCount = 0;
        fullFlickCount = 0;
        pointCount = 0;
        delta = -1;
        score = 0;
    }
    /// <summary>
    /// 加分
    /// </summary>
    /// <param name="noteType">音符类型</param>
    /// <param name="noteJudge">判定等级</param>
    /// <param name="isEarly">是否过早</param>
    public void AddScore(NoteType noteType, NoteJudge noteJudge, bool isEarly)
    {
        switch (noteJudge)
        {
            case NoteJudge.Perfect://完美
                AddScorePerfect(noteType);
                break;
            case NoteJudge.Good://好
                AddScoreGood(noteType, isEarly);
                break;
            case NoteJudge.Bad://坏
                AddScoreBad(noteType, isEarly);
                break;
            case NoteJudge.Miss://小姐
                AddScoreMiss(noteType);
                break;
        }
        UIManager.Instance.ChangeComboAndScoreText(Combo, Score);
    }
    /// <summary>
    /// 加Miss分
    /// </summary>
    /// <param name="noteType"></param>
    private void AddScoreMiss(NoteType noteType)
    {
        switch (noteType)
        {
            case NoteType.Tap:
                tapMiss++;
                Combo = 0;
                break;
            case NoteType.Hold:
                holdMiss++;
                Combo = 0;
                break;
            case NoteType.Drag:
                dragMiss++;
                Combo = 0;
                break;
            case NoteType.Flick:
                flickMiss++;
                Combo = 0;
                break;
            case NoteType.Point:
                pointMiss++;
                Combo = 0;
                break;
            case NoteType.FullFlickPink:
                fullFlickMiss++;
                Combo = 0;
                break;
            case NoteType.FullFlickBlue:
                fullFlickMiss++;
                Combo = 0;
                break;
            default:
                Debug.LogError($"如果你看到这条消息，请截图发给花水终，这有助于我们改进游戏！\n" +
                    $"分数系统出错！\n" +
                    $"错误点：加分方法出错！" +
                    $"错误类型：小姐判定，没找到音符类型");
                break;
        }
    }
    /// <summary>
    /// 加Bad分
    /// </summary>
    /// <param name="noteType"></param>
    /// <param name="isEarly"></param>
    private void AddScoreBad(NoteType noteType, bool isEarly)
    {
        switch (noteType)
        {
            case NoteType.Tap:
                tapBad++;
                switch (isEarly)
                {
                    case true:
                        tapEarly_bad++;
                        break;
                    case false:
                        tapLate_bad++;
                        break;
                }
                Combo = 0;
                break;
            case NoteType.Point:
                pointBad++;
                switch (isEarly)
                {
                    case true:
                        pointEarly_bad++;
                        break;
                    case false:
                        pointLate_bad++;
                        break;
                }
                break;
            default:
                Debug.LogError($"如果你看到这条消息，请截图发给花水终，这有助于我们改进游戏！\n" +
                    $"分数系统出错！\n" +
                    $"错误点：加分方法出错！" +
                    $"错误类型：坏判定，没找到音符类型");
                break;
        }
    }
    /// <summary>
    /// 加Good分
    /// </summary>
    /// <param name="noteType"></param>
    /// <param name="isEarly"></param>
    private void AddScoreGood(NoteType noteType, bool isEarly)
    {
        switch (noteType)
        {
            case NoteType.Tap:
                tapGood++;
                switch (isEarly)
                {
                    case true:
                        tapEarly_good++;
                        break;
                    case false:
                        tapLate_good++;
                        break;
                }
                Combo++;
                break;
            case NoteType.Hold:
                holdGood++;
                switch (isEarly)
                {
                    case true:
                        holdEarly_good++;
                        break;
                    case false:
                        holdLate_good++;
                        break;
                }
                Combo++;
                break;
            case NoteType.Point:
                pointGood++;
                switch (isEarly)
                {
                    case true:
                        pointEarly_good++;
                        break;
                    case false:
                        pointLate_good++;
                        break;
                }
                Combo++;
                break;
            default:
                Debug.LogError($"如果你看到这条消息，请截图发给花水终，这有助于我们改进游戏！\n" +
                    $"分数系统出错！\n" +
                    $"错误点：加分方法出错！" +
                    $"错误类型：好判定，没找到音符类型");
                break;
        }
    }
    /// <summary>
    /// 加Perfect分
    /// </summary>
    /// <param name="noteType"></param>
    private void AddScorePerfect(NoteType noteType)
    {
        switch (noteType)
        {
            case NoteType.Tap:
                tapPerfect++;
                Combo++;
                break;
            case NoteType.Hold:
                holdPerfect++;
                Combo++;
                break;
            case NoteType.Drag:
                dragPerfect++;
                Combo++;
                break;
            case NoteType.Flick:
                flickPerfect++;
                Combo++;
                break;
            case NoteType.Point:
                pointPerfect++;
                Combo++;
                break;
            case NoteType.FullFlickPink:
                fullFlickPerfect++;
                Combo++;
                break;
            case NoteType.FullFlickBlue:
                fullFlickPerfect++;
                Combo++;
                break;
            default:
                Debug.LogError($"如果你看到这条消息，请截图发给花水终，这有助于我们改进游戏！\n" +
                    $"分数系统出错！\n" +
                    $"错误点：加分方法出错！" +
                    $"错误类型：完美判定，没找到音符类型");
                break;
        }
    }
}
[Serializable]
public class Chapter
{
    public string chapterName;
    public string[] musicPath;
}
