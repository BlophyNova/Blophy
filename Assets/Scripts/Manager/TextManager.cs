using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Blophy.Chart;
public class TextManager : MonoBehaviourSingleton<TextManager>
{
    public RectTransform textCanvas;
    public Text[] texts;
    public int lastIndex = 0;//上次召唤到Note[]列表的什么位置了，从上次的位置继续
    public ObjectPoolQueue<TextController> textObjectPool;
    public void Init(Text[] texts)
    {
        this.texts = texts;
        textObjectPool = new(AssetManager.Instance.text, 0, textCanvas);
        if (texts == null || texts.Length == 0)
        {
            Destroy(textCanvas.gameObject);
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        float currentTime = (float)ProgressManager.Instance.CurrentTime;

        for (int i = lastIndex; i < texts.Length; i++)
        {
            if (texts[i].startTime < currentTime)
            {
                textObjectPool.PrepareObject().Init(texts[i], textObjectPool);
                lastIndex++;
            }
            else
            {
                break; // 如果当前文本还没有开始时间，则跳出循环
            }

        }
    }
}
