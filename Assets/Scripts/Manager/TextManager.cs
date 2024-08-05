using Controller;
using Data.ChartData;
using UnityEngine;
using UtilityCode.ObjectPool;
using UtilityCode.Singleton;
namespace Manager
{
    public class TextManager : MonoBehaviourSingleton<TextManager>
    {
        public RectTransform textCanvas;
        public Text[] texts;
        public int lastIndex;//上次召唤到Note[]列表的什么位置了，从上次的位置继续
        public ObjectPoolQueue<TextController> textObjectPool;
        public void Init(Text[] text)
        {
            this.texts = text;
            textObjectPool = new ObjectPoolQueue<TextController>(AssetManager.Instance.text, 0, textCanvas);
            if( text != null && text.Length != 0 )
                return;
            Destroy(textCanvas.gameObject);
            Destroy(gameObject);
        }
        private void Update()
        {
            float currentTime = (float)ProgressManager.Instance.CurrentTime;

            for (int i = lastIndex; i < texts.Length; i++)
            {
                if( !(texts[i].startTime < currentTime) )
                {
                    break;// 如果当前文本还没有开始时间，则跳出循环
                }
                textObjectPool.PrepareObject().Init(texts[i], textObjectPool);
                lastIndex++;

            }
        }
    }
}
