using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UtilityCode.Singleton;
using Random = System.Random;
namespace Scenes.Loading
{
    public class Controller : MonoBehaviourSingleton<Controller>
    {
        public Image background;
        private Action action;
        public float animationDuration = 1f; // 引入的控制动画显示时间的变量
        public TextMeshProUGUI tip;
        public int sleepTime = 2000;
        public void StartLoad()
        {
            StartCoroutine(IncreaseAlphaAndSetTips());
        }
        private IEnumerator IncreaseAlphaAndSetTips()
        {
            background.gameObject.SetActive(true);
            Color backgroundColor = Color.black;
            Color tipColor = Color.white;
            backgroundColor.a = 0;
            tipColor.a = 0;
            background.color = backgroundColor;
            tip.color = tipColor;
            float elapsedTime = 0f;
            TextAsset textAssets = Resources.Load<TextAsset>("Tips/tips");
            string text = textAssets.text;
            string[] tips = text.Split('\n');
            Random rnd = new Random();
            int index = rnd.Next(0, tips.Length);
            string tipText = tips[index];
            tip.text = "|Tip: " + tipText;
            tip.gameObject.SetActive(true);
            while (elapsedTime <= animationDuration * 2)
            {
                if (elapsedTime <= animationDuration){
                    backgroundColor.a = Mathf.Clamp01(elapsedTime / animationDuration);
                    background.color = backgroundColor;
                }
                if(elapsedTime > animationDuration)
                {
                    tipColor.a = Mathf.Clamp01(elapsedTime / animationDuration - 1);
                    tip.color = tipColor;
                }
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            action?.Invoke();
        }
        private void CompleteLoad()
        {
            Thread.Sleep(sleepTime);
            StartCoroutine(DecreaseAlpha());
        }
        private IEnumerator DecreaseAlpha()
        {
            Color backgroundColor = Color.black;
            Color tipColor = Color.white;
            float elapsedTime = 0f;
            while (elapsedTime <= animationDuration * 2)
            {
                Debug.Log("elapsedTime" + elapsedTime);
                Debug.Log("deltaTime" + Time.deltaTime);
                if (elapsedTime <= animationDuration)
                {
                    tipColor.a = Mathf.Clamp01(1 - elapsedTime / animationDuration);
                    tip.color = tipColor;
                }
                if (elapsedTime > animationDuration)
                {
                    backgroundColor.a = Mathf.Clamp01(2 - elapsedTime / animationDuration);
                    background.color = backgroundColor;
                }
                elapsedTime += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            background.gameObject.SetActive(false);
            tip.gameObject.SetActive(false);
        }
        public Controller SetLoadSceneByName(string targetSceneName)
        {
            action = () =>
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()).completed += a => 
                    SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Additive).completed += a =>
                    {
                        SceneManager.SetActiveScene(SceneManager.GetSceneByName(targetSceneName));
                        CompleteLoad();
                    };
            };
            return this;
        }
    }
}
