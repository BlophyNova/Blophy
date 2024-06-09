using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Blophy.Chart;
using Newtonsoft.Json;

public class Public_ControlSpace : MonoBehaviour
{
    public Scrollbar verticalBar;
    public float progressBar;
    public int elementCount;
    public float[] allElementDistance;//所有元素标准的距离（0-1之间的数据）
    public float single => 1f / (elementCount - 1);
    public int currentElementIndex = 0;
    public float currentElement = 0;
    // Update is called once per frame
    private void Start()
    {

        OnStart();
        InitAllElementDistance();
        Send();
    }

    protected void InitAllElementDistance()
    {
        allElementDistance = new float[elementCount];//所有元素标准的距离（0-1之间的数据）
        for (int i = 0; i < elementCount; i++)
        {
            allElementDistance[i] = single * i;
        }
    }

    protected virtual void OnStart() { }
    void Update()
    {
        ListUpdate();
        LargeImageUpdate();
    }
    protected virtual void LargeImageUpdate() { }
    protected virtual void ListUpdate() { }
    public virtual void Send() { }
    protected IEnumerator Lerp()
    {
        while (Mathf.Abs(verticalBar.value - currentElement) > .0001f)
        {
            verticalBar.value = Mathf.Lerp(verticalBar.value, currentElement, .1f);
            yield return new WaitForEndOfFrame();
        }
    }
}
