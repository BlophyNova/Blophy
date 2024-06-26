using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Gameplay_Continue : Public_Button
{
    public Image[] allTexture;
    public TextMeshProUGUI pauseText;
    public TextMeshProUGUI bigPauseText;
    public bool isRunning = false;
    public float textAlpha;
    public float bigTextAlpha;
    private void OnEnable()
    {
        for (int i = 0; i < allTexture.Length; i++)
        {
            allTexture[i].color = Color.white;
        }
        pauseText.alpha = textAlpha;
        bigPauseText.alpha = bigTextAlpha;
    }
    // Start is called before the first frame update
    void Start()
    {
        thisButton.onClick.AddListener(() =>
        {
            if (isRunning) return;
            StartCoroutine(Continue());
        });
    }
    IEnumerator Continue()
    {
        isRunning = true;
        Color color = Color.white;
        while (color.a > 0)
        {
            color.a -= Time.deltaTime;
            for (int i = 0; i < allTexture.Length; i++)
            {
                allTexture[i].color = color;
            }
            pauseText.alpha -= Time.deltaTime * textAlpha;
            bigPauseText.alpha -= Time.deltaTime * bigTextAlpha;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForSeconds(1.5f);
        ProgressManager.Instance.ContinuePlay();
        if(!GlobalData.Instance.isAutoplay)
        SpeckleManager.Instance.enabled = true;
        isRunning = false;
        UIManager.Instance.pauseCanvas.gameObject.SetActive(false);
    }
}
