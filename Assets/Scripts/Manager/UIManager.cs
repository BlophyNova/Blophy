using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviourSingleton<UIManager>
{
    public TextMeshProUGUI musicName;
    public TextMeshProUGUI level;
    public TextMeshProUGUI combo;
    public TextMeshProUGUI score;
    [Header("暂停画布")]
    public Canvas pauseCanvas;
    public Image background;
    public void ChangeComboAndScoreText(int rawCombo, float rawScore)
    {
        combo.text = $"{rawCombo}";
        score.text = $"{(int)rawScore:D7}";
    }
}
